using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalWander : MonoBehaviour
{
    public float moveSpeed = 3f;
    private float timer;

    private float waitTime = 3;

    // Duration the animal waits before moving again (random value from min and max)
    public float waitTimeMin = 0;
    public float waitTimeMax = 3;

    public Transform targetA;
    public Transform targetB;

    // Max range of distance dog travels
    public float moveRange;

    //Bounds
    public float minX = -3f;
    public float maxX = 3f;
    public float minY = -3f;
    public float maxY = 3f;

    void Start()
    {
        waitTime = Random.Range(waitTimeMin, waitTimeMax);
        timer = waitTime;

        // Finds new random location to move to within a set of bounds
        //targetB.position = new Vector2(Mathf.Clamp(transform.position.x + Random.Range(-moveRange, moveRange), minX, maxX), 
            //Mathf.Clamp(transform.position.y + Random.Range(-moveRange, moveRange), minY, maxY));
    }

    void Update()
    {
        // Move towards a point
        transform.position = Vector2.MoveTowards(transform.position, targetB.position, Random.Range(1, moveSpeed) * Time.deltaTime);

        // Upon reaching targeted point
        if (Vector2.Distance(transform.position, targetB.position) < 0.2f)
        {

            if (timer <= 0)
            {
                //target.position = new Vector2(Mathf.Clamp(transform.position.x + Random.Range(-moveRange, moveRange), minX, maxX),
                //Mathf.Clamp(transform.position.y + Random.Range(-moveRange, moveRange), minY, maxY)); waitTime = Random.Range(waitTimeMin, waitTimeMax); // Creates a new random waitTime
                transform.position = targetA.position;
                timer = waitTime; // resets timer
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
}
