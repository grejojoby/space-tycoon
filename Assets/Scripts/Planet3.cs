using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Planet3 : MonoBehaviour
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
        basePrice = PlayerPrefs.GetFloat("Planet3basePrice", 1000);
        priceExpo = PlayerPrefs.GetFloat("Planet3priceExpo", 0.95f);
        FuelGenerate = PlayerPrefs.GetFloat("Planet3FuelGenerate", 0);
        UpgradeBuyCount = PlayerPrefs.GetInt("Planet3UpgradeBuyCount", 1);
        PlanetTimer = PlayerPrefs.GetFloat("Planet3PlanetTimer", 100f);
        fuelGenerationPerSec = PlayerPrefs.GetFloat("Planet3fuelGenerationPerSec", 200);
        Planetlevel = PlayerPrefs.GetInt("Planet3Planetlevel", 0);
        upgradeCost = PlayerPrefs.GetFloat("Planet3upgradeCost", 1000);
        automatePlanet = PlayerPrefs.GetInt("Planet3automatePlanet", 0);
        automateCost = PlayerPrefs.GetFloat("Planet3automateCost", 20000);

        baseFuelGenerate = 200;
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
        PlayerPrefs.SetFloat("Planet3basePrice", basePrice);
        PlayerPrefs.SetFloat("Planet3priceExpo", priceExpo);
        PlayerPrefs.SetFloat("Planet3FuelGenerate", FuelGenerate);
        PlayerPrefs.SetInt("Planet3UpgradeBuyCount", UpgradeBuyCount);
        PlayerPrefs.SetFloat("Planet3PlanetTimer", PlanetTimer);
        PlayerPrefs.SetFloat("Planet3fuelGenerationPerSec", fuelGenerationPerSec);
        PlayerPrefs.SetInt("Planet3Planetlevel", Planetlevel);
        PlayerPrefs.SetFloat("Planet3upgradeCost", upgradeCost);
        PlayerPrefs.SetFloat("Planet3automateCost", automateCost);
        PlayerPrefs.SetInt("Planet3automatePlanet", automatePlanet);

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
            upgradeCost += (int)(upgradeCost * 0.625);

        }
    }

    public void AutomatePlanet()
    {
        automatePlanet = 1;
    }
}

