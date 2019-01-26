using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusManager : MonoBehaviour
{
    public float time { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public float DetermineWaitTime()
    {
        if (time == 0.0f) return 10.0f;
        else if (time > 100.0f) return 1.0f;
        return 10.0f / (100 - time);
    }
}
