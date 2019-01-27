using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public Sprite[] dogHeadSprites;

    /*Animal UI*/
    public Image animalHeadImg;
    public Text breedText;
    public Text ageText;
    public Text allergiesText;
    public Text energyText;
    public Text maintenanceText;
    public Text familyFriendlyText;
    public Text petFriendlyText;

    /*Person UI*/



    void Start()
    {
    }

    void Update()
    {
        
    }

    public void OnAnimalUpdate(string breedTxt, string ageTxt, string allergiesTxt, 
        string energyTxt, string maintenanceTxt, string familyFriendlyTxt, string petFriendlyTxt)
    {
        breedText.text = breedTxt;
        ageText.text = ageTxt;
        allergiesText.text = allergiesTxt;
        energyText.text = energyTxt;
        maintenanceText.text = maintenanceTxt;
        familyFriendlyText.text = familyFriendlyTxt;
        petFriendlyText.text = petFriendlyTxt;
}

    public void OnPersonUpdate()
    {
        Stats person = PeopleGenerator.peopleQ[0].GetComponent<Stats>();

    }

    
}
