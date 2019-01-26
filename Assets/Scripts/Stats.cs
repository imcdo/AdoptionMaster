using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [HideInInspector] public float alergy { get; private set; }
    [HideInInspector] public float fancy { get; private set; }
    [HideInInspector] public float energy { get; private set; }
    [HideInInspector] public float sociality { get; private set; }

    private void Awake()
    {
        alergy = Random.value;
        fancy = Random.value;
        energy = Random.value;
        sociality = Random.value;

        // printData();
    }

    // returns the average diferents in 2 diferents stats objects
    public static float StatDif(Stats p1, Stats p2)
    {
        // check if stats are <= 1
        p1.StatAssert();
        p2.StatAssert();

        float alergyDif = Mathf.Abs(p1.alergy - p2.alergy);
        float fancyDif = Mathf.Abs(p1.fancy - p2.fancy);
        float energyDif = Mathf.Abs(p1.energy - p2.energy);
        float socDif = Mathf.Abs(p1.sociality - p2.sociality);

        float avgDif = (alergyDif + fancyDif + energyDif + socDif) / 4;
        Debug.Assert(avgDif <= 1);

        return avgDif;
    }

    // check if all stats are <= 1 as they should be
    public void StatAssert()
    {
        Debug.Assert(alergy <= 1);
        Debug.Assert(fancy <= 1);
        Debug.Assert(energy <= 1);
        Debug.Assert(sociality <= 1);
    }

    private void printData()
    {
        //string name = gameObject.transform.name;
        //Debug.Log(name + "'s alergy: " + alergy);
        //Debug.Log(name + "'s fancy: " + fancy);
        //Debug.Log(name + "'s energy: " + energy);
        //Debug.Log(name + "'s sociality: " + sociality);
    }
}
