using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public Sprite[] dogHeadSprites;

    /*Animal UI*/
    [HideInInspector] public Image animalHeadImg;
    [HideInInspector] public Text breedText;
    [HideInInspector] public Text ageText;
    [HideInInspector] public Text allergiesText;
    [HideInInspector] public Text energyText;
    [HideInInspector] public Text maintenanceText;
    [HideInInspector] public Text familyFriendlyText;
    [HideInInspector] public Text petFriendlyText;
    public GameObject animalCard;

    /*Person UI*/
    [HideInInspector] public Image humanHeadImg;
    [HideInInspector] public Text ageTextHuman;
    [HideInInspector] public Text incomeText;
    [HideInInspector] public Text energyTextHuman;
    [HideInInspector] public Text allergiesTextHuman;
    [HideInInspector] public Text spaceText;
    [HideInInspector] public Text familyext;
    [HideInInspector] public Text petText;
    public GameObject humanCard;


    void Start()
    {
        var animalchildren = animalCard.GetComponentsInChildren<Text>();
        Debug.Assert(animalchildren.Length >= 7);

        breedText = animalchildren[0];
        ageText = animalchildren[1];
        allergiesText = animalchildren[2];
        energyText = animalchildren[3];
        maintenanceText = animalchildren[4];
        familyFriendlyText = animalchildren[5];
        petFriendlyText = animalchildren[6];

        animalHeadImg = animalCard.GetComponentInChildren<Image>();

        var humanchildren = humanCard.GetComponentsInChildren<Text>();
        Debug.Assert(humanchildren.Length >= 7);

        ageTextHuman = humanchildren[0];
        incomeText = humanchildren[1];
        energyTextHuman = humanchildren[2];
        allergiesTextHuman = humanchildren[3];
        spaceText = humanchildren[4];
        familyext = humanchildren[5];
        petText = humanchildren[6];

        humanHeadImg = humanCard.GetComponentInChildren<Image>();
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
