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

    [HideInInspector] public Sprite[] peopleFaceSprites;

    private GameObject emotionImage;

    Sprite[] emotions;
    void Start()
    {
        // emotionImage = GameObject.Find("EmotionImage");
        emotionImage = GameObject.Find("EmotionImage");
        //emotionImage.SetActive(false);
        emotions = Resources.LoadAll<Sprite>("Emotions");
        //emotionImage = humanCard.transform.parent.gameObject.GetComponentInChildren<Animator>().gameObject;
        var animalchildren = animalCard.GetComponentsInChildren<Text>();
        Debug.Assert(animalchildren.Length >= 7);

        breedText = animalchildren[0];
        ageText = animalchildren[1];
        allergiesText = animalchildren[2];
        energyText = animalchildren[3];
        maintenanceText = animalchildren[4];
        familyFriendlyText = animalchildren[5];
        petFriendlyText = animalchildren[6];

        animalHeadImg = animalCard.transform.GetChild(0).GetComponent<Image>();

        var humanchildren = humanCard.GetComponentsInChildren<Text>();
        Debug.Assert(humanchildren.Length >= 7);

        ageTextHuman = humanchildren[0];
        incomeText = humanchildren[1];
        energyTextHuman = humanchildren[2];
        allergiesTextHuman = humanchildren[3];
        spaceText = humanchildren[4];
        familyext = humanchildren[5];
        petText = humanchildren[6];

        humanHeadImg = humanCard.transform.GetChild(0).GetComponent<Image>();

        peopleFaceSprites = Resources.LoadAll<Sprite>("PeopleHeads");
    }

    void Update()
    {
        
    }
    public void EmotionAnimation(float diff)
    {
        if (diff <= 0.33f)
        {
            //show heart
            emotionImage.GetComponent<Image>().sprite = emotions[0];
            emotionImage.GetComponent<Animator>().SetTrigger("play");
            //emotionImage.SetActive(true);

        }
        else if (diff <= 0.66f)
        {
            //show blah
            emotionImage.GetComponent<Image>().sprite = emotions[1];
            emotionImage.GetComponent<Animator>().SetTrigger("play");
            //emotionImage.SetActive(true);

        }
        else
        {
            //show bad
            emotionImage.GetComponent<Image>().sprite = emotions[2];
            emotionImage.GetComponent<Animator>().SetTrigger("play");
            //emotionImage.SetActive(true);

        }
    }
    public void OnAnimalUpdate(string breedTxt, string ageTxt, string allergiesTxt, 
        string energyTxt, string maintenanceTxt, string familyFriendlyTxt, string petFriendlyTxt, Sprite dogSprite)
    {
        breedText.text = breedTxt;
        ageText.text = ageTxt;
        allergiesText.text = allergiesTxt;
        energyText.text = energyTxt;
        maintenanceText.text = maintenanceTxt;
        familyFriendlyText.text = familyFriendlyTxt;
        petFriendlyText.text = petFriendlyTxt;
        animalCard.SetActive(true);

        animalHeadImg.sprite = dogSprite;
    }

    public void OnAnimalReleased()
    {
        animalCard.SetActive(false);

    }

    public void OnPersonUpdate()
    {
        //Stats personStats = PeopleGenerator.peopleQ[0].GetComponent<Stats>();
        Person person = PeopleGenerator.peopleQ[0].GetComponent<Person>();
        humanHeadImg.sprite= peopleFaceSprites [person.spriteIndex];

        //set all the text

        ageTextHuman.text = "Age: " + person.ageText;
        incomeText.text = "Income: "+ person.incomeText;
        energyTextHuman.text = "Energy: " + person.energyText;
        allergiesTextHuman.text = "Allergies: " + person.allergyText;
        spaceText.text= "Space: " + person.spaceText;
        familyext.text = "Family: " + person.familyText;
        petText.text = "Pet: " + person.petsText;

    humanCard.SetActive(true);
    }
    public void OnPersonReleased()
    {
        humanCard.SetActive(false);
    }
    
    public int AssignFaceToPerson()
    {
        int randomIndex = Random.Range(0, 6);
        return randomIndex;
    }
}
