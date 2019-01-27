using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    string[] allergyCategories = { "None", "Mild", "High" };
    string[] incomeCategories = { "Low", "Medium", "High"};
    string[] spaceCategories = { "Apartment", "House", "Ranch"};
    string[] ageCategories = { "Young", "Middle Age", "Elderly" };
    string[] energyCategories = { "Tired", "Average", "Energetic" };
    string[] familyCategories = { "Single", "Married", "Kids"};
    string[] petsCategories = { "None", "Few", "Lots" };


    public string allergyText;
    public string incomeText;
    public string spaceText;
    public string ageText;
    public string energyText;
    public string familyText;
    public string petsText;
    public int spriteIndex;


    /** People

    Alergy
    Fancy - income & space
    Energy - Age & energy
    Sociability - family & other pets

    */
    private int numSubCategory = 2;

    private float allergyRating =0;

    private float incomeRating = 0;
    private float spaceRating = 0;

    private float ageRating = 0;
    private float energyRating = 0;

    private float familyRating = 0;
    private float petsRating = 0;

    Stats personStats;

    UIManagerScript uiMan;
    void Start()
    {
        uiMan = GetComponent<UIManagerScript>();
        personStats = GetComponent<Stats>();
        Debug.Log("new person spawned");
       // Debug.Log(gameObject.name + personStats.fancy);
        GetSubValues(personStats);

        // Debug.Log(allergyRating + " " + incomeRating + " " + spaceRating + " " + ageRating + " "+ energyRating + " " + familyRating + " " + petsRating);

        allergyText = SetStringCategory(allergyCategories, allergyRating);
        incomeText = SetStringCategory(incomeCategories, incomeRating);
        spaceText = SetStringCategory(spaceCategories, spaceRating);
        ageText = SetStringCategory(ageCategories, ageRating);
        energyText = SetStringCategory(energyCategories, energyRating);
        familyText = SetStringCategory(familyCategories, familyRating);
        petsText = SetStringCategory(petsCategories, petsRating);
        spriteIndex = Random.Range(0, 6);
        // Debug.Log(allergyText + " " + incomeText + " " + spaceText + " " + ageText + " " + energyText + " " + familyText + " " + petsText);
        if (PeopleGenerator.peopleQ.Count == 1)
        {
            //call UI manager to show player card with info.
            FindObjectOfType<UIManagerScript>().OnPersonUpdate();
        }

        FindObjectOfType<QueueManager>().AddToQueue(spriteIndex);

    }


    public string SetStringCategory(string [] category, float value)
    {
        if(value < 0.33)
        {
            return category[0];
        }else if (value < 0.66)
        {
            return category[1];
        }
        else
        {
            return category[2];
        }
    }

    public void GetSubValues(Stats stats) {

        //get all 4 stats here and find the sub stats for each. 

        allergyRating = stats.alergy;
        CalculateMath(stats.fancy,ref incomeRating,ref spaceRating);
        CalculateMath(stats.energy, ref ageRating, ref energyRating);
        CalculateMath(stats.sociality, ref familyRating, ref petsRating);


        /*
         x = (2 * coreAt)
         y = rand(0,x)
         s1 = max(min(1,y), min(1, (#subsets - y)))
         s2 = #sub - s1
            */



        //Debug.Log(gameObject.name + s1);
        //Debug.Log(gameObject.name + s2);

    }

    private void CalculateMath(float coreAttribute, ref float s1, ref float s2)
    {
        float x = 2 * coreAttribute;
        float y = Random.Range(0, x);
        s1 = Mathf.Max(Mathf.Min(1, y), Mathf.Min(1, (x - y)));
        s2 = x - s1;
    }

}
