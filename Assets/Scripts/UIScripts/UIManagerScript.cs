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
    public GameObject animalCard;

    /*Person UI*/
    public Image humanHeadImg;
    public Text ageTextHuman;
    public Text incomeText;
    public Text energyTextHuman;
    public Text allergiesTextHuman;
    public Text spaceText;
    public Text familyext;
    public Text petText;
    public GameObject humanCard;


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
        animalCard.SetActive(true);
    }

    public void OnAnimalReleased()
    {
        animalCard.SetActive(false);

    }

    public void OnPersonUpdate()
    {
        //Stats person = PeopleGenerator.peopleQ[0].GetComponent<Stats>();
        humanCard.SetActive(true);
    }
    public void OnPersonReleased()
    {
        humanCard.SetActive(false);
    }
    
}
