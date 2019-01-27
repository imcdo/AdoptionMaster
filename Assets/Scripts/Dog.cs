using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{

    AudioSource[] audioSrcs;

    string[] energyCategories = { "Relaxed", "Moderate", "Hyper" };
    string[] breedCategories = { "Chow Chow", "Terreir", "Pomeranian", "Poodle", "Shiba", "Doberman", "Chihuahua" };
    string[] upKeepCategories = { "Economical", "Reasonable", "High Maintence" };
    string[] allergyCategories = { "Hypoallergic", "Light Shedding", "Sheds" };
    string[] familyCategories = { "Antisocial", "Accepting", "Loving" };
    string[] petCategories = { "Introvert", "Average", "Friendly" };
    string[] ageCategories = { "Puppy", "Mid-Life", "Old Dog" };

    public Animator anim;
    public SpriteRenderer sr;

    public enum moveDirection { UP, DOWN, LEFT, RIGHT }

    public string allergyText;
    public string breedText;
    public string upKeepText;
    public string energyText;
    public string ageText;
    public string familyText;
    public string petsText;

    private int numSubCategory = 2;

    private float allergyRating = 0;

    private float breedRating = 0;
    private float upKeepRating = 0;

    private float ageRating = 0;
    private float energyRating = 0;

    private float familyRating = 0;
    private float petsRating = 0;
    /*
        Animals

        Allergy - Allergy
        Fancy - breed & maintaincnence
        Energy - Age & energy
        Sociability - people friendly & pet friendly
    */


    private Stats stats;
    private GameStatusManager gameStatusManager;
    
    [Tooltip("Max dog moveSpeed")]
    [SerializeField] private float maxSpeed = 1;
    public float moveSpeed = 3;
    public float runSpeed = 6;
    [SerializeField] public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        gameStatusManager = FindObjectOfType<GameStatusManager>();
        audioSrcs = GetComponents<AudioSource>();

        //Get all information about the damn dogs.

        GetSubValues(stats);
        //Debug.Log("*********" +allergyRating + " " + breedRating + " " + upKeepRating + " " + ageRating + " "+ energyRating + " " + familyRating + " " + petsRating);

        allergyText = SetStringCategory(allergyCategories, allergyRating);
        breedText = stats.dogBreed.ToString();
        upKeepText = SetStringCategory(upKeepCategories, upKeepRating);
        energyText = SetStringCategory(energyCategories, energyRating);
        ageText = SetStringCategory(ageCategories, ageRating);
        familyText = SetStringCategory(familyCategories, familyRating);
        petsText = SetStringCategory(petCategories, petsRating);

        //Debug.Log(allergyText + " " + breedText + " " + upKeepText + " " + energyText + " " + ageText + " " + familyText + " " + petsText);

        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // determine moveSpeed for the dog
        moveSpeed = maxSpeed * (.5f + (stats.energy / 2));
        Debug.Assert(moveSpeed < maxSpeed);

        // give a random init move direction
        moveDir = Random.onUnitSphere;


    }

    public string SetStringCategory(string[] category, float value)
    {
        if (value < 0.33)
        {
            return category[0];
        }
        else if (value < 0.66)
        {
            return category[1];
        }
        else
        {
            return category[2];
        }
    }

    public void GetSubValues(Stats stats)
    {

        //get all 4 stats here and find the sub stats for each. 

        allergyRating = stats.alergy;
        //CalculateMath(stats.fancy, ref incomeRating, ref spaceRating);
        CalculateMath(stats.energy, ref ageRating, ref energyRating);
        CalculateMath(stats.sociality, ref familyRating, ref petsRating);

        breedRating = Stats.breedStatDict[stats.dogBreed];
        upKeepRating = ((stats.fancy * 2)) - breedRating;
        /*
         x = (2 * coreAt)
         y = rand(0,x)
         s1 = max(min(1,y), min(1, (#subsets - y)))
         s2 = #sub - s1
            */



        //Debug.Log(gameObject.name + s1);
        //Debug.Log(gameObject.name + s2);

    }

    private void CalculateMath(float coreAttribute, ref float s1, ref float s2)
    {
        float x = 2 * coreAttribute;
        float y = Random.Range(0, x);
        s1 = Mathf.Max(Mathf.Min(1, y), Mathf.Min(1, (x - y)));
        s2 = x - s1;
    }

    private static moveDirection GetMoveDir(Vector3 vec)
    {
        vec.Normalize();
        if(Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
        {
            // Left or right
            return (vec.x > 0) ? moveDirection.LEFT : moveDirection.RIGHT;
        }
        else
        {
            // Up or down
            return (vec.y > 0) ? moveDirection.UP : moveDirection.DOWN;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(pos.x <= GameStatusManager.minX && moveDir.x < 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.right);
        }
        if (pos.x >= GameStatusManager.maxX && moveDir.x > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.left);
        }
        if (pos.y <= GameStatusManager.minY && moveDir.y < 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.up);
        } 
        if (pos.y >= GameStatusManager.maxY && moveDir.y > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.down);
        }

        //print(GetMoveDir(moveDir));
        if (GetMoveDir(moveDir) == moveDirection.UP)
        {
            anim.SetBool("isMovingSide", false);
            anim.SetBool("isMovingDown", false);
            anim.SetBool("isMovingUp", true);
        }
        else if (GetMoveDir(moveDir) == moveDirection.DOWN)
        {
            anim.SetBool("isMovingSide", false);
            anim.SetBool("isMovingDown", true);
            anim.SetBool("isMovingUp", false);
        }

        if (GetMoveDir(moveDir) == moveDirection.RIGHT)
        {
            anim.SetBool("isMovingUp", false);
            anim.SetBool("isMovingDown", false);
            anim.SetBool("isMovingSide", true);
            sr.flipX = true;
        }
        else if (GetMoveDir(moveDir) == moveDirection.LEFT)
        {
            anim.SetBool("isMovingUp", false);
            anim.SetBool("isMovingDown", false);
            anim.SetBool("isMovingSide", true);
            sr.flipX = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("was a wall");

            Debug.DrawRay(transform.position, moveDir.normalized);

            RaycastHit2D[] hits = new RaycastHit2D[2];
            
            if (0 != Physics2D.Raycast(transform.position, moveDir, new ContactFilter2D(), hits))
            {
                Debug.Log("Point of contact: " + hits[0].point);
            }
            else
            {
                Debug.Log("oof");
            }
            Vector3 normal = hits[0].normal;
            moveDir = Vector3.Reflect(moveDir, normal);
        }
    }
}
