using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStateDisplay : MonoBehaviour
{

    private GameStatusManager gsm;
    private Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        gsm = FindObjectOfType<GameStatusManager>();
        moneyText = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + gsm.money;
    }
}
