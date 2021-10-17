using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Planet1 : MonoBehaviour
{
    public float basePrice;
    public float priceExpo; 
    public float FuelGenerate;
    public float baseFuelGenerate;

    public int timemultiply;
    public static int Planetlevel;
    public float PlanetTimer;
    float CurrentTimer;
    public static bool StartTimer = false;
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
        basePrice =  PlayerPrefs.GetFloat("Planet1basePrice",5);
        priceExpo = PlayerPrefs.GetFloat("Planet1priceExpo",0.88f);
        FuelGenerate = PlayerPrefs.GetFloat("Planet1FuelGenerate", 0);
        UpgradeBuyCount = PlayerPrefs.GetInt("Planet1UpgradeBuyCount", 1);
        PlanetTimer = PlayerPrefs.GetFloat("Planet1PlanetTimer", 2f);
        fuelGenerationPerSec = PlayerPrefs.GetFloat("Planet1fuelGenerationPerSec", 1);
        Planetlevel = PlayerPrefs.GetInt("Planet1Planetlevel", 0);
        upgradeCost = PlayerPrefs.GetFloat("Planet1upgradeCost", 5);
        automatePlanet = PlayerPrefs.GetInt("Planet1automatePlanet", 0);
        automateCost = PlayerPrefs.GetFloat("Planet1automateCost", 5000);
        timemultiply = 1;
        PlanetTimer *= timemultiply;
        CurrentTimer = 0;
        baseFuelGenerate = 1;
        //offlineCollection = ((FuelGenerate / PlanetTimer) * GameManager.seconds);
    }

    void Update()
    {
        LevelText.text = "Lvl:" + Planetlevel.ToString("F0");
        upgradeCostText.text = upgradeCost.ToString("F0");
        FuelGenerationText.text = FuelGenerate.ToString("F0");
        AutomateCostText.text = automateCost.ToString("F0");

        if (automatePlanet == 0 && GameManager.coins >= automateCost && Planetlevel != 0)
            automateButton.interactable = true;
        else
            automateButton.interactable = false;

        if (Planetlevel == 0 || StartTimer == true)
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

        if (StartTimer && automatePlanet==0)
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
                CurrentTimer = 0f;
                StartTimer = true;
                
            }
        }
        ProgressSlider.value = CurrentTimer / PlanetTimer;

        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("Planet1basePrice", basePrice);
        PlayerPrefs.SetFloat("Planet1priceExpo", priceExpo);
        PlayerPrefs.SetFloat("Planet1FuelGenerate", FuelGenerate);
        PlayerPrefs.SetInt("Planet1UpgradeBuyCount", UpgradeBuyCount);
        PlayerPrefs.SetFloat("Planet1PlanetTimer", PlanetTimer);
        PlayerPrefs.SetFloat("Planet1fuelGenerationPerSec", fuelGenerationPerSec);
        PlayerPrefs.SetInt("Planet1Planetlevel", Planetlevel);
        PlayerPrefs.SetFloat("Planet1upgradeCost", upgradeCost);
        PlayerPrefs.SetFloat("Planet1automateCost", automateCost);
        PlayerPrefs.SetInt("Planet1automatePlanet", automatePlanet);
        PlayerPrefs.Save();
    }
    public void Generate()
    {
        if (automatePlanet == 0)
        {
            CurrentTimer = 0f;
            StartTimer = true;
        }
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
            upgradeCost += (int)(upgradeCost * 0.575);
        }
    }
    public void AutomatePlanet()
    {
        automatePlanet = 1;
    }
}

