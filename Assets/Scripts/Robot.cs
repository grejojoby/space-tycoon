using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public float CurrentTimer;
    public bool StartTimer=true;
    public float RobotTimer;
    public int RobotLevel;
    public float CoinGenerate;
    public float priceExpo;
    public float basePrice;
    public float upgradeCostMetal;
    public float upgradeCostFuel;

    public TextMeshProUGUI RobotLevelText;
    public TextMeshProUGUI CoinGenerateText;
    public TextMeshProUGUI UpgradeCostMetalText;
    public TextMeshProUGUI UpgradeCostFuelText;

    public Button upgradebuttonMetal;
    public Button upgradebuttonFuel;

    void Start()
    {
        basePrice = PlayerPrefs.GetFloat("RobotbasePrice", 100);
        priceExpo = PlayerPrefs.GetFloat("RobotpriceExpo", 0.9f);
        CoinGenerate = PlayerPrefs.GetFloat("RobotCoinGenerate", 10);
        RobotTimer = PlayerPrefs.GetFloat("RobotTimer", 60);
        RobotLevel = PlayerPrefs.GetInt("RobotLevel", 1);
        upgradeCostMetal = PlayerPrefs.GetFloat("RobotupgradeCostMetal", 20);
        upgradeCostFuel = PlayerPrefs.GetFloat("RobotupgradeCostFuel", 5000);
        CurrentTimer = 0;
    }

    void Update()
    {
        if (GameManager.metal >= upgradeCostMetal)
            upgradebuttonMetal.interactable = true;
        else
            upgradebuttonMetal.interactable = false;

        if (GameManager.fuel >= upgradeCostFuel)
            upgradebuttonFuel.interactable = true;
        else
            upgradebuttonFuel.interactable = false;

        RobotLevelText.text = "Robot Level:" + RobotLevel.ToString("F0");
        CoinGenerateText.text = "Coins Per Min: " + CoinGenerate.ToString("F0");
        UpgradeCostMetalText.text =  upgradeCostMetal.ToString("F0");
        UpgradeCostFuelText.text =  upgradeCostFuel.ToString("F0");

        if (StartTimer)
        {
            CurrentTimer += Time.deltaTime;
            if (CurrentTimer > RobotTimer)
            {
                CurrentTimer = 0f;
                if(RobotLevel>0 && PlayerPrefs.GetInt("Planet1Planetlevel", 0)>=1)
                    GameManager.coins += (CoinGenerate);
            }
        }
        SaveData();

    }
    public void RobotUpgradeMetal()
    {
        if (GameManager.metal >= upgradeCostMetal)
        {
            RobotLevel++;
            GameManager.metal -= (int)(upgradeCostMetal);
            upgradeCostMetal += (upgradeCostMetal*RobotLevel);
            upgradeCostFuel += (upgradeCostFuel*RobotLevel);
            CoinGenerate += CoinGenerate;
        }

    }

    public void RobotUpgradeFuel()
    {
        if (GameManager.fuel >= upgradeCostFuel)
        {
            RobotLevel++;
            GameManager.fuel -= (int)(upgradeCostFuel);
            upgradeCostMetal += (upgradeCostMetal * RobotLevel);
            upgradeCostFuel += (upgradeCostFuel * RobotLevel);
            CoinGenerate += CoinGenerate;
        }

    }

    public void SaveData()
    {

        PlayerPrefs.SetFloat("RobotbasePrice", basePrice);
        PlayerPrefs.SetFloat("RobotpriceExpo", priceExpo);
        PlayerPrefs.SetFloat("RobotCoinGenerate", CoinGenerate);
        PlayerPrefs.SetFloat("RobotTimer", RobotTimer);
        PlayerPrefs.SetFloat("RobotLevel", RobotLevel);
        PlayerPrefs.SetFloat("RobotupgradeCostFuel", upgradeCostFuel);
        PlayerPrefs.SetFloat("RobotupgradeCostMetal", upgradeCostMetal);
        PlayerPrefs.Save();

    }
}
