using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    public static List<GameObject> peopleQ;
    [HideInInspector] public bool spawningPeople { get; private set; } = true;
    GameStatusManager gm;
    public static float maxPeopleQLength = 10;

    private void Awake()
    {
        gm = FindObjectOfType<GameStatusManager>().GetComponent<GameStatusManager>();

        Debug.Assert(peopleQ == null);
        peopleQ = new List<GameObject>();

        StartCoroutine("PersonSpawner");
    }

    public GameObject GeneratePerson()
    {
        GameObject person = new GameObject();
        Stats stats = person.AddComponent<Stats>();
        SpriteRenderer sr = person.AddComponent<SpriteRenderer>();
        Person ps = person.AddComponent<Person>();
        stats.dogBreed = Stats.breed.Human;
        ps.transform.name = "Person";
        peopleQ.Add(person);
        return person;
    }

    //public GameObject spawnPerson()
    //{
    //    GameObject person = GeneratePerson();
    //    GameObject ourPerson = Instantiate(person);
    //    peopleQ.Enqueue(ourPerson);
    //    return ourPerson;
    //}

    IEnumerator PersonSpawner()
    {
        while (true)
        { 
            if(peopleQ.Count >= maxPeopleQLength) { spawningPeople = false; }
            else { spawningPeople = true; }
            if(spawningPeople)
            {
                GeneratePerson();

                yield return new WaitForSeconds(gm.DetermineWaitTime());
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
