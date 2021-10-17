using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Planet4 : MonoBehaviour
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
    public Button automateButton;

    public Button generatebutton;
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
        basePrice = PlayerPrefs.GetFloat("Planet4basePrice", 10000);
        priceExpo = PlayerPrefs.GetFloat("Planet4priceExpo", 0.99f);
        FuelGenerate = PlayerPrefs.GetFloat("Planet4FuelGenerate", 0);
        UpgradeBuyCount = PlayerPrefs.GetInt("Planet4UpgradeBuyCount", 1);
        PlanetTimer = PlayerPrefs.GetFloat("Planet4PlanetTimer", 500f);
        fuelGenerationPerSec = PlayerPrefs.GetFloat("Planet4fuelGenerationPerSec", 500);
        Planetlevel = PlayerPrefs.GetInt("Planet4Planetlevel", 0);
        upgradeCost = PlayerPrefs.GetFloat("Planet4upgradeCost", 10000);
        automatePlanet = PlayerPrefs.GetInt("automatePlanet", 0);
        automateCost = PlayerPrefs.GetFloat("automateCost", 100000);

        timemultiply = 1; 
        PlanetTimer *= timemultiply;
        baseFuelGenerate = 500;
        CurrentTimer = 0;

        offlineCollection += ((FuelGenerate / PlanetTimer) * GameManager.seconds);

    }

    void Update()
    {
        LevelText.text = "Lvl:" + Planetlevel.ToString("F0");
        upgradeCostText.text =  upgradeCost.ToString("F0");
        FuelGenerationText.text = FuelGenerate.ToString("F0");
        AutomateCostText.text = automateCost.ToString("F0");


        if (automatePlanet == 0 && GameManager.coins >= automateCost && Planetlevel!=0)
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
                GameManager.fuel += FuelGenerate;

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
        PlayerPrefs.SetFloat("Planet4basePrice", basePrice);
        PlayerPrefs.SetFloat("Planet4priceExpo", priceExpo);
        PlayerPrefs.SetFloat("Planet4FuelGenerate", FuelGenerate);
        PlayerPrefs.SetInt("Planet4UpgradeBuyCount", UpgradeBuyCount);
        PlayerPrefs.SetFloat("Planet4PlanetTimer", PlanetTimer);
        PlayerPrefs.SetFloat("Planet4fuelGenerationPerSec", fuelGenerationPerSec);
        PlayerPrefs.SetInt("Planet4Planetlevel", Planetlevel);
        PlayerPrefs.SetFloat("Planet4upgradeCost", upgradeCost);
        PlayerPrefs.SetFloat("Planet4automateCost", automateCost);
        PlayerPrefs.SetInt("Planet4automatePlanet", automatePlanet);

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
            upgradeCost += (int)(upgradeCost * 0.655);

        }
    }

    public void AutomatePlanet()
    {
        automatePlanet = 1;
    }
}

