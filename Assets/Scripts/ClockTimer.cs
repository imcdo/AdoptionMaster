using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimer : MonoBehaviour
{
    public float startDay = 8;
    public float endDay = 20;
    public float daySpeed = 1;

    public float gameLengthInSeconds = 50;
    private float currentTime;
    private Image clockImage;
    public UnityEngine.Sprite[] clock;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        clockImage = GameObject.Find("ClockImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += (Time.deltaTime / gameLengthInSeconds) * daySpeed;
        print(currentTime);
      
        int idx = (int)Mathf.Floor(currentTime * clock.Length);
        if (idx < clock.Length)
        {
            clockImage.sprite = clock[idx];
        }
        if (currentTime >= 1)
        {
            currentTime = 0;
            print("Day Ends");
        }
    }
}
