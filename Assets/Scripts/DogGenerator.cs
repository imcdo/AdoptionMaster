using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DogGenerator : MonoBehaviour
{



    public GameObject GenerateDog()
    {
        GameObject dog = new GameObject();
        Stats stats = dog.AddComponent<Stats>();
        SpriteRenderer sr = dog.AddComponent<SpriteRenderer>();
        Dog ds = dog.AddComponent<Dog>();
        DogBehavior db = dog.AddComponent<DogBehavior>();

        return dog;
    }
}

