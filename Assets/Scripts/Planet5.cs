using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Planet5 : MonoBehaviour
{

    // public static Text levelText;
    public float basePrice;
    public float priceExpo;
    public float MetalGenerate;
    public float baseMetalGenerate;


    public int timemultiply;
    public int Planetlevel;
    public float PlanetTimer;
    float CurrentTimer;
    bool StartTimer = false;
    public float MetalGenerationPerSec;
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
        basePrice = PlayerPrefs.GetFloat("Planet5basePrice", 50000);
        priceExpo = PlayerPrefs.GetFloat("Planet5priceExpo", 1.5f);
        MetalGenerate = PlayerPrefs.GetFloat("Planet5MetalGenerate", 0);
        UpgradeBuyCount = PlayerPrefs.GetInt("Planet5UpgradeBuyCount", 1);
        PlanetTimer = PlayerPrefs.GetFloat("Planet5PlanetTimer", 1800f);
        MetalGenerationPerSec = PlayerPrefs.GetFloat("Planet5MetalGenerationPerSec", 0);
        Planetlevel = PlayerPrefs.GetInt("Planet5Planetlevel", 0);
        upgradeCost = PlayerPrefs.GetFloat("Planet5upgradeCost", 50000);
        automatePlanet = PlayerPrefs.GetInt("Planet5automatePlanet", 0);
        automateCost = PlayerPrefs.GetFloat("Planet5automateCost", 1000000);

        timemultiply = 1; 
        PlanetTimer *= timemultiply;
        CurrentTimer = 0;
        baseMetalGenerate = 100;

        offlineCollection = ((MetalGenerate / PlanetTimer) * GameManager.seconds);

    }

    void Update()
    {
        LevelText.text = "Lvl:" + Planetlevel.ToString("F0");
        upgradeCostText.text =  upgradeCost.ToString("F0");
        FuelGenerationText.text = MetalGenerate.ToString("F0");
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
                GameManager.metal += MetalGenerate;

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
                GameManager.fuel += (MetalGenerate);
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
        PlayerPrefs.SetFloat("Planet5basePrice", basePrice);
        PlayerPrefs.SetFloat("Planet5priceExpo", priceExpo);
        PlayerPrefs.SetFloat("Planet5MetalGenerate", MetalGenerate);
        PlayerPrefs.SetInt("Planet5UpgradeBuyCount", UpgradeBuyCount);
        PlayerPrefs.SetFloat("Planet5PlanetTimer", PlanetTimer);
        PlayerPrefs.SetFloat("Planet5MetalGenerationPerSec", MetalGenerationPerSec);
        PlayerPrefs.SetInt("Planet5Planetlevel", Planetlevel);
        PlayerPrefs.SetFloat("Planet5upgradeCost", upgradeCost);
        PlayerPrefs.SetFloat("Planet5automateCost", automateCost);
        PlayerPrefs.SetInt("Planet5automatePlanet", automatePlanet);

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
            MetalGenerate = baseMetalGenerate * Planetlevel;
            //upgradeCost = upgradeCost - basePrice*Math.Pow(priceExpo,Planet1level)*((Math.Pow(basePrice,UpgradeBuyCount)-1))/(priceExpo-1);
            //Debug.Log(basePrice * Math.Pow(priceExpo, Planet1level) * ((Math.Pow(basePrice, UpgradeBuyCount) - 1)) / (priceExpo - 1));
            //upgradeCost += (upgradeCost*priceExpo) + (basePrice-(upgradeCost * priceExpo)%basePrice);
            upgradeCost += (int)(upgradeCost * 0.715);

        }
    }

    public void AutomatePlanet()
    {
        automatePlanet = 1;
    }
}

