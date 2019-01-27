using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStatusManager : MonoBehaviour
{
    [HideInInspector] public float time { get; private set; }
    [HideInInspector] public int score { get; set; }

    GameObject selectedDog;
    GameObject grabedDog;

    [HideInInspector] public static float maxX;
    [HideInInspector] public static float minX;
    [HideInInspector] public static float maxY;
    [HideInInspector] public static float minY;

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
            {Stats.breed.Dober , 0.6f}
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        uiman = FindObjectOfType<UIManagerScript>().GetComponent<UIManagerScript>();
        dg = FindObjectOfType<DogGenerator>().GetComponent<DogGenerator>();
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        MouseInteractions();
    }

    public float DetermineWaitTime()
    {
        if (time == 0.0f) return 10.0f;
        else if (time > 100.0f) return 1.0f;
        return 10.0f / (100 - time);
    }

    private void MouseInteractions()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
            Debug.Assert(grabedDog == null);
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
                if(s != null)
                {
                    uiman.animalAttribute1.text = "" + s.alergy;
                    uiman.animalAttribute2.text = "" + s.fancy;
                    uiman.animalAttribute3.text = "" + s.energy;
                    uiman.animalAttribute4.text = "" + s.sociality;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
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
                    GameObject person = PeopleGenerator.peopleQ.Dequeue();

                    float dif = Stats.StatDif(person.GetComponent<Stats>(), grabedDog.GetComponent<Stats>());
                    money += (1 - dif) * maxPayment;

                    DogGenerator.Dogs.Remove(grabedDog);
                    Destroy(grabedDog);
                    dg.GenerateDog();
                }
                else if (PeopleGenerator.peopleQ.Count == 0)
                {
                    grabedDog.transform.position = new Vector3(Random.Range(GameStatusManager.maxX, GameStatusManager.minX), Random.Range(GameStatusManager.maxY, GameStatusManager.minY));
                }

            }
            grabedDog = null;
        }
    }
}
