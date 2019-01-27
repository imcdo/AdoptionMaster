using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    private int maxLimit = 5;
    public Transform[] queuePoints;
    public GameObject spawnPoint;
    public GameObject playerPrefab;

    public Queue<GameObject> waitingLine;

    private int queueCounter;

    private GameObject addTemp;
    private GameObject removeTemp;
    private bool isFull;
    private bool isEmpty;

    private GameObject[] playerPrefabs;

    private void Awake()
    {
        playerPrefabs = Resources.LoadAll<GameObject>("PeoplePrefabs");
        addTemp = new GameObject();
        waitingLine = new Queue<GameObject>();
        queueCounter = 0;
        isFull = false;
        isEmpty = true;
    }

    private void Update()
    {
        
    }

    public void AddToQueue(int playerIndex)
    {
        if (!isFull)
        {
            //addTemp = Instantiate(playerPrefab, queuePoints[queueCounter].position, queuePoints[queueCounter].rotation);

            //addTemp.AddComponent<QueueMovement>();

            addTemp = Instantiate(playerPrefabs[playerIndex], spawnPoint.transform.position, spawnPoint.transform.rotation);
            addTemp.GetComponent<PlayerMovement>().target = queuePoints[queueCounter].transform.position;
            //addTemp.GetComponent<QueueMovement>().Target = queuePoints[queueCounter].transform;



            waitingLine.Enqueue(addTemp);
           
            queueCounter++;
            isEmpty = false;

            if(queueCounter >= maxLimit)
            {
                isFull = true;
            }
        }
        else
        {
            Debug.Log("Queue is full!");
        }
    }

    public void RemoveFromQueue()
    {
        if (!isEmpty)
        {
            Debug.Log("Start removing");
            queueCounter--;
            isFull = false;

            removeTemp = (GameObject)waitingLine.Dequeue();
            GameObject.Destroy(removeTemp);
            ResetQueue();
            if (queueCounter == 0)
            {
                isEmpty = true;
            }
            
        }
        else
        {
            Debug.Log("Is empty");
        }
       

    }

    private void ResetQueue()
    {
        GameObject[] tempList = new GameObject[waitingLine.Count];
        waitingLine.CopyTo(tempList, 0);

        for (int x = 0; x < tempList.Length; x++) {
            //tempList[x].transform.position = queuePoints[x].position;
            tempList[x].GetComponent<PlayerMovement>().target = queuePoints[x].position;
        }
    }
    //private int maxLimit = 5;
    //public Transform[] queuePoints;
    //public GameObject playerPrefab;

    //private List<GameObject> playerList;

    //private int currentQueueCounter;
    //private bool isFull;
    //private bool isEmpty;

    //void Start()
    //{
    //    playerList = new List<GameObject>();
    //    currentQueueCounter = 0;
    //    isFull = false;
    //    isEmpty = true;
    //}

    //void Update()
    //{
    //    //AddToQueue();
    //}

    //public void RemoveFromQueue()
    //{

    //    if (!isEmpty)
    //    {
    //        //remove the first person on the queue. 
    //        //decrease counter
    //        //play remove animation

    //        //playerList.RemoveAt(0);
    //        Destroy(playerList[0]);

    //        currentQueueCounter--;

    //        if(currentQueueCounter == 0)
    //        {
    //            isEmpty = true;
    //        }
    //        //move all the others to next position and 
    //        for (int i = 1; i < playerList.Count - 1; i++) {
    //            playerList[i].gameObject.transform.position = playerList[i - 1].gameObject.transform.position;

    //            //playerList.inde
    //        }
    //        //animate movement.

    //    }
    //    else
    //    {
    //        Debug.Log("Empty");
    //    }



    //}


    //public void AddToQueue()
    //{
    //    if (!isFull)
    //    {
    //        //Add the person to the next available queue position.
    //        //peopleSprites[currentQueueCount].transform.position = queuePoints[currentQueueCount].position;
    //        playerList.Add(Instantiate(playerPrefab, queuePoints[currentQueueCounter].position, queuePoints[currentQueueCounter].rotation));

    //        currentQueueCounter++;
    //        isEmpty = false;
    //        if(currentQueueCounter >= maxLimit)
    //        {
    //            isFull = true;
    //        }
    //        //animate walking in this function as well. 
    //    }
    //    else
    //    {
    //        Debug.Log("Is full");
    //    }
    //}
}
