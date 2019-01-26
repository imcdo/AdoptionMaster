using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DogGenerator : MonoBehaviour
{
    static List<GameObject> Dogs;

    Sprite[] Sprites;

    [Tooltip("number of dogs at the begining of the game")]
    [SerializeField] private int numberOfStartingDogs = 10;

    private void Awake()
    {
        Dogs = new List<GameObject>();
        Sprites = Resources.LoadAll<Sprite>("DogSprites");
        Debug.Assert(Sprites != null);

        for (int i = 0; i < numberOfStartingDogs; i++)
        {
            SpawnDog();
        }

    }

    public GameObject GenerateDog()
    {
        Debug.Log(LayerMask.NameToLayer("Animals"));
        gameObject.layer = LayerMask.NameToLayer("Animals");
        Debug.Log("layer : " + gameObject.layer);
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

        return dog;
    }

    public GameObject SpawnDog()
    {
        GameObject dog = GenerateDog();
        // Instatiate the game object
        GameObject ourDog = Instantiate(dog);

        // add the instantiated game object to the list
        Dogs.Add(ourDog);
        return ourDog;
    }
}

