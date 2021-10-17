using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button up, down, seven, confirm, reset,x10;
    public TextMeshProUGUI diceText;
    public TextMeshProUGUI coinsText,usrtext;
    public int dice =0;
    public float coins = 100f;
    public float usercoin = 10f;
    public bool flagup, flagdown, flagseven;
    void Start()
    {
        flagdown = flagseven = flagup = true;
        coins = PlayerPrefs.GetFloat("coins", 100);
        //coins = 20;
    }

    // Update is called once per frame
    void Update()
    { 
        diceText.text = "DICE " + dice.ToString();
        coinsText.text = " "+coins.ToString("F0");
        usrtext.text = "BET: "+usercoin.ToString("F0");
        up.interactable = flagup;
        down.interactable = flagdown;
        seven.interactable = flagseven;
        if (flagup && flagdown && flagseven)
            confirm.interactable = false;
        else if (!flagup && !flagdown && !flagseven)
            confirm.interactable = false;
        else if (coins - usercoin >= 0)
            confirm.interactable = true;
        else
            confirm.interactable = false;
        if (coins >= usercoin)
        {
            x10.interactable = true;
        }
    }
    public void Change()
    {
        if (coins-usercoin >= 0)
        {
            coins -= usercoin;
            
             dice = Random.Range(2, 13);
             diceText.text = "DICE " + dice.ToString();
            if (dice == 7 && flagseven)
            {
                coins += usercoin * 3;
            }
            if (dice > 7 && flagup)
            {
                coins += usercoin * 2;
            }
            if (dice < 7 && flagdown)
            {
                coins += usercoin * 2;
            }
            flagdown = flagseven = flagup = true;
        }
        PlayerPrefs.SetFloat("coins", coins);

    }

    public void Onclickup()
    {
        flagdown = flagseven = false;
    }
    public void Onclickdown()
    {
        flagseven = flagup = false;
    }
    public void Onclickseven()
    {
        flagdown = flagup = false;
    }
    public void Reset()
    {
        flagdown = flagseven = flagup = true;
        usercoin = 10;
    }
    public void Multiply()
    {
        usercoin *= 10;
        if(coins<usercoin)
        {
            flagdown = flagseven = flagup = false;
            x10.interactable = false;
        }
    }
    public void Divide()
    {
        if(usercoin>10)
        usercoin /= 10;
        if (coins >= usercoin)
        {
            flagdown = flagseven = flagup = true;
        }
    }
}