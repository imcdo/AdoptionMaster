using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class GameStatusManager : MonoBehaviour
{
    public QueueManager cuemanager;

    [HideInInspector] public float currentTime { get; private set; }
    [HideInInspector] public int score { get; set; }

    GameObject selectedDog;
    GameObject grabedDog;

    [HideInInspector] public static float maxX;
    [HideInInspector] public static float minX;
    [HideInInspector] public static float maxY;
    [HideInInspector] public static float minY;

    // time
    public float startDay = 8;
    public float endDay = 20;
    public float daySpeed = 1;
    public float dayLengthInSeconds = 90;

    public int numDaysPerRentPayment = 3;
    public float rentAmount = 3150;

    [Tooltip("the distribution of people over the day")]
    public AnimationCurve peopleDistribution;
    public float peoplePerNewDayModifier = .7f;
    [HideInInspector] public int dayNumber = 0;

    UIManagerScript uiman;

    [HideInInspector] public float money = 100;

    [Tooltip("The max amount of money for a mach")]
    [SerializeField] private float maxPayment = 100;

    private DogGenerator dg;


    private void Awake()
    {
        Camera main = Camera.main;

        var topLeft = main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var bottomRight = main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));

        minX = topLeft.x;
        maxX = bottomRight.x;

        minY = bottomRight.y;
        maxY = Mathf.Abs(topLeft.y);

        Debug.Log(topLeft + " " + bottomRight);
        Debug.Log(minX + " " + maxX + " " + minY + " " + maxY);

        Stats.breedStatDict = new Dictionary<Stats.breed, float>()
        {
            {Stats.breed.Shiba , 0.4f},
            {Stats.breed.Doberman , 0.6f},
            {Stats.breed.ChowChow , 0.7f},
            {Stats.breed.Terreir , 0.3f},
            {Stats.breed.Pomeranian , 0.2f},
            {Stats.breed.Poodle , 0.5f},
            {Stats.breed.Chihuahua , 0.1f},
            {Stats.breed.Human ,0f}

        };
    }

    // Start is called before the first frame update
    void Start()
    {
        uiman = FindObjectOfType<UIManagerScript>().GetComponent<UIManagerScript>();
        dg = FindObjectOfType<DogGenerator>().GetComponent<DogGenerator>();
        cuemanager = FindObjectOfType<QueueManager>();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += (Time.deltaTime / dayLengthInSeconds) * daySpeed;
        
        if (currentTime >= 1)
        {
            currentTime = 0;
            dayNumber++;
            print("Day Ends");
            DayStart();
        }
        MouseInteractions();
    }

    public float DetermineWaitTime()
    {

        float fracDay = currentTime / dayLengthInSeconds;
        var wait = (dayLengthInSeconds  * (((peoplePerNewDayModifier / (1 + dayNumber) * peopleDistribution.Evaluate(fracDay)) * 0.7f + Random.value * 0.3f))) / 2;
        Debug.Log("Wait time running " + wait);
        return wait;
    }

    private void MouseInteractions()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
    
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if(hit.transform != null) { 
                GameObject Animal = hit.transform.gameObject;
                grabedDog = Animal;
                Debug.Assert(Animal != null);
                Debug.Log(Animal);

                Animal.layer = LayerMask.NameToLayer("Mouse");
                Debug.Log("layer :" + Animal.layer);
                DogBehavior db = grabedDog.GetComponent<DogBehavior>();
                if (db != null)
                {
                    db.grabbed = true;
                    Debug.Log("Dog grabbed");
                }

                Stats s = grabedDog.GetComponent<Stats>();
                Dog d = grabedDog.GetComponent<Dog>();
                if(s != null)
                {
                    string breed, age, allergy, energy, maintenance, family, pet;

                    breed = "Breed: " + d.breedText;
                    age = "Age: " + d.ageText;
                    allergy = "Allergies: " + d.allergyText;
                    energy = "Energy: " + d.energyText;
                    maintenance = "Maintenance: " + d.upKeepText;
                    family = "Family Friendly: " + d.familyText;
                    pet = "Pet Friendly: " + d.petsText;


                    Sprite dogImage = d.GetComponent<SpriteRenderer>().sprite;
                    Debug.Log("Sprite to be rendered name is " + dogImage.name);

                    uiman.OnAnimalUpdate(breed, age, allergy, energy,
                        maintenance, family, pet, dogImage);

                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            uiman.OnAnimalReleased();
            // TODO: put dog in proper location
            if (grabedDog != null)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 point = ray.GetPoint(0);
                point.z = 0;

                grabedDog.layer = LayerMask.NameToLayer("Animals");

                DogBehavior db = grabedDog.GetComponent<DogBehavior>();
                if (db != null)
                {
                    db.grabbed = false;
                }
                if (grabedDog.transform.position.y < minY && PeopleGenerator.peopleQ.Count != 0)
                {
                    GameObject person = PeopleGenerator.peopleQ[0];
                    PeopleGenerator.peopleQ.RemoveAt(0);
                    Debug.Log("REMOVE PEOPLE");
                    cuemanager.RemoveFromQueue();
                    uiman.OnPersonReleased();
                    float dif = Stats.StatDif(person.GetComponent<Stats>(), grabedDog.GetComponent<Stats>());
                    money += (1 - dif) * maxPayment;

                    DogGenerator.Dogs.Remove(grabedDog);
                    Destroy(grabedDog);

                    //IF we JUST removed a person from the queue
                    //show the next person's details in the UI IF count is NOT zero

                    if(PeopleGenerator.peopleQ.Count != 0)
                    {
                        uiman.OnPersonUpdate();
                    }
                }
                else if (PeopleGenerator.peopleQ.Count == 0)
                {
                    if (grabedDog.transform.position.y < minY) { 
                        grabedDog.transform.position = new Vector3(grabedDog.transform.position.x, GameStatusManager.minY, 0);
                    }
                }

            }
            grabedDog = null;
        }
    }

    public void DayStart()
    {
        DogGenerator.RefillDogs();
        int numInQ = PeopleGenerator.peopleQ.Count;

        PeopleGenerator.peopleQ.Clear();

        for (int i = 0; i < numInQ; i++) cuemanager.RemoveFromQueue();
        uiman.OnPersonReleased();
        if (dayNumber % numDaysPerRentPayment == 0 && dayNumber != 0) money -= rentAmount;
    }
}
