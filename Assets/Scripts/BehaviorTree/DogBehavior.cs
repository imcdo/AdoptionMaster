using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTreeSpace;

public class DogBehavior : MonoBehaviour
{
    private Selector root;
    private Dog ds;
    public bool grabbed = false;

    [Tooltip("an average action timer to base on, will add noise")]
    [SerializeField] float startActionInterval = 5;
    [SerializeField] float playRadius = .5f;



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
        root = new Selector();
        Sequencer seq1 = new Sequencer();
        Randomizer rand1 = new Randomizer();

        root.AddChild(rand1);
        root.AddChild(seq1);

        Condition con1 = new Condition(nearDogs);
        BAction pact = new BAction(Play);
        BAction wact = new BAction(Wander);
        BAction ract = new BAction(Run);
        BAction sact = new BAction(Sleep);

        seq1.AddChild(con1);
        seq1.AddChild(pact);

        rand1.AddChild(wact);
        rand1.AddChild(ract);
        // rand1.AddChild(sact);


        root.AddChild(new BAction(Wander));

    }

    bool nearDogs()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, playRadius);
        List<GameObject> nearDogs = new List<GameObject>();
        foreach(var col in cols)
        {
            if (col.GetComponent<Dog>() != null) nearDogs.Add(col.gameObject);
        }
        
        return nearDogs.Count != 0;
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
            ds.transform.Translate(ds.moveDir * ds.moveSpeed * Time.deltaTime);
            ds.transform.position = new Vector3(ds.transform.position.x, ds.transform.position.y, 0);
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
            ds.transform.Translate(ds.moveDir * ds.runSpeed * Time.deltaTime);
            ds.transform.position = new Vector3(ds.transform.position.x, ds.transform.position.y, 0);
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
        
            return Status.RUNNING;
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
            return Status.RUNNING;
        }
    }
}
