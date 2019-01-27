using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTreeSpace;

public class DogBehavior : MonoBehaviour
{
    private Sequencer root;
    private Dog ds;
    public bool grabbed = false;

    [Tooltip("an average action timer to base on, will add noise")]
    [SerializeField] float startActionInterval = 5;

    private float actionInterval;

    float actionTimer = 0;

    void Awake()
    {
        ds = gameObject.GetComponent<Dog>();
        Debug.Assert(ds != null);

        actionInterval = startActionInterval + 2 - 4 * Random.value;  

        BuildTree();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!grabbed) { 
            actionTimer += Time.deltaTime;
            root.Update();
        } else
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            transform.position = pz;
        }
    }

    private void BuildTree()
    {
        root = new Sequencer();
        root.AddChild(new BAction(Wander));
    }


    
    Status Wander()
    {
        if (actionTimer >= actionInterval)
        {
            ds.moveDir = Random.onUnitSphere;
            actionInterval = startActionInterval + 1 - 2 * Random.value;
            actionTimer = 0;
            return Status.SUCCESS;
        }
        else
        {
            ds.Wander();
            return Status.RUNNING;
        }
    }

    Status Run()
    {
        if (actionTimer >= actionInterval)
        {
            ds.moveDir = Random.onUnitSphere;
            actionInterval = startActionInterval + 1 - 2 * Random.value;
            actionTimer = 0;
            return Status.SUCCESS;
        }
        else
        {
            ds.Wander();
            return Status.RUNNING;
        }
    }

    Status Sleep()
    {
        if (actionTimer >= actionInterval)
        {
            ds.moveDir = Random.onUnitSphere;
            actionInterval = startActionInterval + 1 - 2 * Random.value;
            actionTimer = 0;
            return Status.SUCCESS;
        }
        else
        {
            ds.Wander();
            return Status.RUNNING;
        }
    }

    Status Play()
    {
        if (actionTimer >= actionInterval)
        {
            ds.moveDir = Random.onUnitSphere;
            actionInterval = startActionInterval + 1 - 2 * Random.value;
            actionTimer = 0;
            return Status.SUCCESS;
        }
        else
        {
            ds.Wander();
            return Status.RUNNING;
        }
    }
}
