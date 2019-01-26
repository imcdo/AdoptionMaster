using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTreeSpace;

public class DogBehavior : MonoBehaviour
{
    private Behavior root;
    private Dog ds;

    void Awake()
    {
        ds = gameObject.GetComponent<Dog>();
        Debug.Assert(ds != null);

        BuildTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildTree()
    {

    }
}
