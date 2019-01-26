using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    /*Animal UI*/
    public Image animalImage;
    public Text animalAttribute1;
    public Text animalAttribute2;
    public Text animalAttribute3;
    public Text animalAttribute4;

    /*Person UI*/
    public Image personImage;
    public Text personAttribute1;
    public Text personAttribute2;
    public Text personAttribute3;
    public Text personAttribute4;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnAnimalUpdate()
    {
        Debug.Log("clicked");
    }

    public void OnPersonUpdate()
    {
        Debug.Log("clicked");
    }
}
