using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DogGenerator : MonoBehaviour
{
    public static List<GameObject> Dogs;

    GameObject[] DogPrefabs;

    [Tooltip("number of dogs at the begining of the game")]
    [SerializeField] private int numberOfStartingDogs = 10;

    private void Awake()
    {
        Dogs = new List<GameObject>();
        DogPrefabs = Resources.LoadAll<GameObject>("DogPrefabs");
        Debug.Assert(DogPrefabs != null);
        
    }
    
    private void Start()
    {
        for (int i = 0; i < numberOfStartingDogs; i++)
        {
            GenerateDog();
        }
    }

    public GameObject GenerateDog()
    {
        
        Random spriteSelector = new Random();
        GameObject dog = new GameObject();
        Stats stats = dog.AddComponent<Stats>();
        SpriteRenderer sr = dog.AddComponent<SpriteRenderer>();
        sr.sprite = Sprites[Random.Range(0, Sprites.Length)];
        Dog ds = dog.AddComponent<Dog>();
        DogBehavior db = dog.AddComponent<DogBehavior>();
        dog.transform.name = "Dog";
        BoxCollider2D bc = dog.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        Rigidbody2D rb = dog.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        dog.transform.position = new Vector3(Random.Range(GameStatusManager.maxX, GameStatusManager.minX), Random.Range(GameStatusManager.maxY, GameStatusManager.minY), 0);
        gameObject.layer = LayerMask.NameToLayer("Animals");

        Dogs.Add(dog);
        return dog;
    }
}

