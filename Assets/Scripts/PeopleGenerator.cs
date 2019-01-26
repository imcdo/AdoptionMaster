using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    public static Queue<GameObject> peopleQ;
    [HideInInspector] public bool spawningPeople { get; private set; } = true;
    GameStatusManager gm;
    public static float maxPeopleQLength = 10;

    private void Awake()
    {
        gm = FindObjectOfType<GameStatusManager>().GetComponent<GameStatusManager>();

        Debug.Assert(peopleQ == null);
        peopleQ = new Queue<GameObject>();

        StartCoroutine("spawnPerson");
    }

    public GameObject GeneratePerson()
    {
        GameObject person = new GameObject();
        Stats stats = person.AddComponent<Stats>();
        SpriteRenderer sr = person.AddComponent<SpriteRenderer>();
        Person ps = person.AddComponent<Person>();
        return person;
    }

    IEnumerator spawnPerson()
    {
        while (true)
        { 
            if(peopleQ.Count >= maxPeopleQLength) { spawningPeople = false; }
            else { spawningPeople = true; }
            if(spawningPeople)
            { 
                GameObject person = GeneratePerson();
                peopleQ.Enqueue(person);
                Debug.Log("Created a Person");
                
                // TODO: Instatiate the person
                yield return new WaitForSeconds(gm.DetermineWaitTime());
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
