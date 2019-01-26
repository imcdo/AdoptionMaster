using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStatusManager : MonoBehaviour
{
    [HideInInspector] public float time { get; private set; }
    [HideInInspector] public int score { get; set; }
    [HideInInspector] public int money { get; set; }

    GameObject selectedDog;
    GameObject grabedDog;


    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        MouseInteractions();
    }

    public float DetermineWaitTime()
    {
        if (time == 0.0f) return 10.0f;
        else if (time > 100.0f) return 1.0f;
        return 10.0f / (100 - time);
    }

    private void MouseInteractions()
    {
        Debug.Assert(grabedDog == null);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
            Debug.Assert(grabedDog == null);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            GameObject Animal = hit.transform.gameObject;
            grabedDog = Animal;
            Debug.Assert(Animal != null);
            Debug.Log(Animal);
            DogBehavior db = grabedDog.GetComponent<DogBehavior>();
            if (db != null)
            {
                db.grabbed = true;
                Debug.Log("Dog grabbed");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            // TODO: put dog in proper location
            if (grabedDog != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 point = ray.GetPoint(0);
                point.z = 0;

                DogBehavior db = grabedDog.GetComponent<DogBehavior>();
                if (db != null)
                {
                    db.grabbed = false;
                }
            }
            grabedDog = null;
        }
    }
}
