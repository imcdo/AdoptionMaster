using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    public QueueManager cuemanager;
    public UIManagerScript uiManagerScript;
    public static List<GameObject> peopleQ;
    [HideInInspector] public bool spawningPeople { get; private set; } = true;
    GameStatusManager gm;
    public static float maxPeopleQLength = 5;

    private bool isFirstSpawn = true;

    private void Start()
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
        
        //IF the count of the queue is ZERO that means the VERY next person has to be show on the UI
        if(peopleQ.Count == 0)
        {
            //call UI manager to show player card with info.
            uiManagerScript.OnPersonUpdate();
        }

        peopleQ.Add(person);
        cuemanager.AddToQueue();
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
            if(peopleQ.Count >= maxPeopleQLength)
            { spawningPeople = false;
            }else
            { spawningPeople = true; }

            if(spawningPeople)
            {
                //remove the first immediate spawned person so that it lines up with the queue animation
                if (isFirstSpawn)
                {
                    //do nothing
                    isFirstSpawn = false;
                }
                else
                {
                    GeneratePerson();

                }

                yield return new WaitForSeconds(gm.DetermineWaitTime());
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
