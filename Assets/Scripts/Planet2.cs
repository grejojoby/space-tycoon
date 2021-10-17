using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Planet2 : MonoBehaviour
{

    // public static Text levelText;
    public float basePrice;
    public float priceExpo;
    public float FuelGenerate;
    public float baseFuelGenerate;

    public int timemultiply;
    public int Planetlevel;
    public float PlanetTimer;
    float CurrentTimer;
    bool StartTimer = false;
    public float fuelGenerationPerSec;
    public float upgradeCost;

    public Button upgradebutton;
    public Button generatebutton;
    public Button automateButton;

    public Slider ProgressSlider;

    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI FuelGenerationText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI AutomateCostText;


    public int UpgradeBuyCount;
    public int automatePlanet;
    public float automateCost;
    public static float offlineCollection;



    void Start()
    {
        basePrice = PlayerPrefs.GetFloat("Planet2basePrice", 200);
        priceExpo = PlayerPrefs.GetFloat("Planet2priceExpo", 0.92f);
        FuelGenerate = PlayerPrefs.GetFloat("Planet2FuelGenerate", 0);
        UpgradeBuyCount = PlayerPrefs.GetInt("Planet2UpgradeBuyCount", 1);
        PlanetTimer = PlayerPrefs.GetFloat("Planet2PlanetTimer", 15f);
        fuelGenerationPerSec = PlayerPrefs.GetFloat("Planet2fuelGenerationPerSec", 50);
        Planetlevel = PlayerPrefs.GetInt("Planet2Planetlevel", 0);
        upgradeCost = PlayerPrefs.GetFloat("Planet2upgradeCost", 200);
        automatePlanet = PlayerPrefs.GetInt("Planet2automatePlanet", 0);
        automateCost = PlayerPrefs.GetFloat("Planet2automateCost", 10000);

        baseFuelGenerate = 50;
        timemultiply = 1;
        PlanetTimer *= timemultiply;
        CurrentTimer = 0;
        offlineCollection = ((FuelGenerate / (PlanetTimer)) * GameManager.seconds);


    }

    void Update()
    {
        LevelText.text = "Lvl:" + Planetlevel.ToString("F0");
        upgradeCostText.text =  upgradeCost.ToString("F0");
        FuelGenerationText.text = FuelGenerate.ToString("F0");
        AutomateCostText.text = automateCost.ToString("F0");


        if (automatePlanet == 0 && GameManager.coins >= automateCost && Planetlevel != 0)
            automateButton.interactable = true;
        else
            automateButton.interactable = false;

        if (Planetlevel == 0 || StartTimer==true)
            generatebutton.interactable = false;
        else
            generatebutton.interactable = true;
        if (GameManager.fuel < upgradeCost)
        {
            upgradebutton.interactable=false;
        }
        else
        {
            upgradebutton.interactable = true; 

        }

        if (StartTimer && automatePlanet == 0)
        {

            CurrentTimer += Time.deltaTime;
            if (CurrentTimer > PlanetTimer)
            {
                StartTimer = false;
                CurrentTimer = PlanetTimer;
                GameManager.fuel += (FuelGenerate);

                ProgressSlider.value = CurrentTimer / PlanetTimer;

            }

        }
        if (automatePlanet == 1)
        {
            ColorBlock colors = generatebutton.colors;
            colors.disabledColor = new Color32(255, 255, 255, 255);
            generatebutton.colors = colors;
            CurrentTimer += Time.deltaTime;
            if (CurrentTimer > PlanetTimer)
            {
                GameManager.fuel += (FuelGenerate);
                //ProgressSlider.value = CurrentTimer / PlanetTimer;
                StartTimer = true;
                CurrentTimer = 0f;
            }
        }
        ProgressSlider.value = CurrentTimer / PlanetTimer;

        SaveData();

    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("Planet2basePrice", basePrice);
        PlayerPrefs.SetFloat("Planet2priceExpo", priceExpo);
        PlayerPrefs.SetFloat("Planet2FuelGenerate", FuelGenerate);
        PlayerPrefs.SetInt("Planet2UpgradeBuyCount", UpgradeBuyCount);
        PlayerPrefs.SetFloat("Planet2PlanetTimer", PlanetTimer);
        PlayerPrefs.SetFloat("Planet2fuelGenerationPerSec", fuelGenerationPerSec);
        PlayerPrefs.SetInt("Planet2Planetlevel", Planetlevel);
        PlayerPrefs.SetFloat("Planet2upgradeCost", upgradeCost);
        PlayerPrefs.SetFloat("Planet2automateCost", automateCost);
        PlayerPrefs.SetInt("Planet2automatePlanet", automatePlanet);

        PlayerPrefs.Save();

    }

    public void Generate()
    {
        CurrentTimer = 0f;
        
            StartTimer = true;
        
    }
   
    public void PlanetUpgrade()
    {
            if (GameManager.fuel >= upgradeCost)
            {
                Planetlevel++;
                GameManager.fuel -= (int)(upgradeCost);
            FuelGenerate = baseFuelGenerate * Planetlevel;
            //upgradeCost = upgradeCost - basePrice*Math.Pow(priceExpo,Planet1level)*((Math.Pow(basePrice,UpgradeBuyCount)-1))/(priceExpo-1);
            //Debug.Log(basePrice * Math.Pow(priceExpo, Planet1level) * ((Math.Pow(basePrice, UpgradeBuyCount) - 1)) / (priceExpo - 1));
            //upgradeCost += (upgradeCost*priceExpo) + (basePrice-(upgradeCost * priceExpo)%basePrice);
            upgradeCost += (int)(upgradeCost * 0.595);

        }

    }

    public void AutomatePlanet()
    {
        automatePlanet = 1;
    }
}

