using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{

    private int maxLimit = 10;
    public Transform[] queuePoints;
    private int currentQueueCount;
    private bool isFull;

    void Start()
    {
        currentQueueCount = 0;
        isFull = false;
    }

    void Update()
    {
        
    }

    public void AddToQueue()
    {
        if (!isFull)
        {
            //add to queue

        }
        else
        {
            Debug.Log("Is full");
        }
    }
}
