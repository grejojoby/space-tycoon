using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public static float coins;
    public static float metal;
    public static float fuel;
    public static float cost;
    private DateTime TimeStarted;
    public static int seconds;

    public static float OfflineFuel;
    public static float OfflineMetal;
    public static float OfflineCoin;

    public Slider offlineSlider;
    public TextMeshProUGUI coinsText ;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI fuelText;
    public TextMeshProUGUI OfflineTextFuel;
    public TextMeshProUGUI OfflineTextMetal;
    public TextMeshProUGUI OfflineTextCoin;

    public Button OfflineButton;


    public int tutorialTemp;
    public float Planet1Offline;
    public float Planet2Offline;
    public float Planet3Offline;
    public float Planet4Offline;
    public float Planet5Offline;

    public GameObject OfflineAlert;
    public GameObject AudioSourceObject;
    public Slider AudioSliderObject;


    //tutorial
    public GameObject earningsClose;
    public GameObject collectBtn;
    public GameObject EarningsClick;
    public GameObject Autoclose;
    public GameObject PlanetAutoClick;
    public GameObject AutoClick;
    public GameObject RobotClose;
    public GameObject RobotUpgrade;
    public GameObject RobotClick;
    public GameObject updateHand;
    public GameObject PlanetClick;

    void Start()
    {
        tutorialTemp = PlayerPrefs.GetInt("tutorialTemp", -1); 
        AudioSourceObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume", 1f);
        AudioSliderObject.value = PlayerPrefs.GetFloat("volume", 1f);
        Debug.Log("vol: "+PlayerPrefs.GetFloat("volume", 1f));

        coins = PlayerPrefs.GetFloat("coins", 5000);
        metal = PlayerPrefs.GetFloat("metal", 20);
        fuel = PlayerPrefs.GetFloat("fuel", 50);
        coinsText.text =coins.ToString("F0");
        metalText.text =metal.ToString("F0");
        fuelText.text =fuel.ToString("F0");
        string TimeStartedstr = PlayerPrefs.GetString("last_online_time", DateTime.UtcNow.ToString());
        TimeStarted = DateTime.Parse(TimeStartedstr);
        double result = DateTime.UtcNow.Subtract(TimeStarted).TotalSeconds;
        seconds = (int)result;
        OfflineButton.interactable = true;
        if (seconds > 4000)
            seconds = 4000;

        Planet1Offline = (PlayerPrefs.GetFloat("Planet1FuelGenerate", 0) / PlayerPrefs.GetFloat("Planet1PlanetTimer", 2f)) * seconds * PlayerPrefs.GetInt("Planet1automatePlanet", 0);
        Planet2Offline = (PlayerPrefs.GetFloat("Planet2FuelGenerate", 0) / PlayerPrefs.GetFloat("Planet2PlanetTimer", 30f)) * seconds * PlayerPrefs.GetInt("Planet2automatePlanet", 0);
        Planet3Offline = (PlayerPrefs.GetFloat("Planet3FuelGenerate", 0) / PlayerPrefs.GetFloat("Planet3PlanetTimer", 300f)) * seconds * PlayerPrefs.GetInt("Planet3automatePlanet", 0);
        Planet4Offline = (PlayerPrefs.GetFloat("Planet4FuelGenerate", 0) / PlayerPrefs.GetFloat("Planet4PlanetTimer", 1800f)) * seconds * PlayerPrefs.GetInt("Planet4automatePlanet", 0);
        Planet5Offline = (PlayerPrefs.GetFloat("Planet5MetalGenerate", 0) / (PlayerPrefs.GetFloat("Planet4PlanetTimer", 3600f))) * seconds * PlayerPrefs.GetInt("Planet5automatePlanet", 0);
       
        OfflineMetal = PlayerPrefs.GetFloat("OfflineMetal", 0);
        OfflineFuel = PlayerPrefs.GetFloat("OfflineFuel", 0);
        OfflineCoin = PlayerPrefs.GetFloat("OfflineCoin", 0);

        OfflineFuel += Planet1Offline + Planet2Offline + Planet3Offline + Planet4Offline;
        OfflineMetal += Planet5Offline;
        OfflineCoin += (PlayerPrefs.GetFloat("RobotCoinGenerate", 10) / PlayerPrefs.GetFloat("RobotTimer", 60)) * seconds;
        PlayerPrefs.SetFloat("OfflineMetal", OfflineMetal);
        PlayerPrefs.SetFloat("OfflineFuel", OfflineFuel);
        PlayerPrefs.SetFloat("OfflineCoin", OfflineCoin);
        //Debug.Log("Seconds: " + seconds);
        //Debug.Log("Offline Fuel: " + OfflineFuel);
        //Debug.Log("Offline Metal: " + OfflineMetal);
        offlineSlider.value = ((float)seconds / 4000);
        //Debug.Log("Slider Val: " + offlineSlider.value + " Seconds : " + seconds +" ACtual Val: " + seconds/100);
        if(OfflineCoin>0 || OfflineFuel>0 || OfflineMetal>0)
        {
            OfflineAlert.SetActive(true);
        }
    }

    void Update()
    {
        //tutorial start
        
        if(Planet1.Planetlevel==0)
        {
            updateHand.SetActive(true);
        }
        else
        {
            updateHand.SetActive(false);
            if (tutorialTemp == -1)
            {
                tutorialTemp = 0;
            }
           
        }
        if (Planet1.Planetlevel == 1 && fuel==45 && Planet1.StartTimer==false)
        {
            PlanetClick.SetActive(true);
        }
        else
        {
            PlanetClick.SetActive(false);
            if(tutorialTemp==0 && Planet1.StartTimer==false && fuel>45)
            {
                tutorialTemp = 1;
            }
            
            
        }
        if(tutorialTemp==1)
        {
            RobotClick.SetActive(true);
        }
        if(tutorialTemp!=1)
        {
            RobotClick.SetActive(false);
        }
        if (tutorialTemp == 2)
            RobotUpgrade.SetActive(true);
        if (tutorialTemp != 2)
            RobotUpgrade.SetActive(false);

        if(tutorialTemp==3)
        {
            RobotClose.SetActive(true);
        }
        if (tutorialTemp != 3)
            RobotClose.SetActive(false);

        if (tutorialTemp == 4)
        {
            AutoClick.SetActive(true);
        }
        if (tutorialTemp != 4)
            AutoClick.SetActive(false);
        if (tutorialTemp == 5)
            PlanetAutoClick.SetActive(true);
        else
            PlanetAutoClick.SetActive(false);
        if (tutorialTemp == 6)
            Autoclose.SetActive(true);
        else
            Autoclose.SetActive(false);
        if (tutorialTemp == 7)
        {
            Debug.Log("here");
            EarningsClick.SetActive(true);
        }
        else
            EarningsClick.SetActive(false);
        if (tutorialTemp == 8)
            collectBtn.SetActive(true);
        else
            collectBtn.SetActive(false);
        if (tutorialTemp == 9)
            earningsClose.SetActive(true);
        else
        {
            earningsClose.SetActive(false);
            PlayerPrefs.SetInt("tutorialTemp", 100);
        }
        if(tutorialTemp<9)
            PlayerPrefs.SetInt("tutorialTemp", tutorialTemp);






        //tutorial end
        coinsText.text =coins.ToString("F0");
        metalText.text =metal.ToString("F0");
        fuelText.text =fuel.ToString("F0");
        OfflineTextFuel.text = "Offline Fuel : " + OfflineFuel.ToString("F0");
        OfflineTextMetal.text = "Offline Metal : " + OfflineMetal.ToString("F0");
        OfflineTextCoin.text = "Offline Coin : " + OfflineCoin.ToString("F0");

        PlayerPrefs.SetFloat("coins", coins);
        PlayerPrefs.SetFloat("metal", metal);
        PlayerPrefs.SetFloat("fuel", fuel);
        PlayerPrefs.Save();

        string currentTime;
        currentTime = DateTime.UtcNow.ToString();//convert current time to string to save
        PlayerPrefs.SetString("last_online_time", currentTime);


        Debug.Log(tutorialTemp);

    }

    public void CollectOffline()
    {
        fuel += OfflineFuel;
        metal += OfflineMetal;
        coins += OfflineCoin;
        seconds = 0;
        OfflineButton.interactable = false;
        Debug.Log("Offline COllected!");
        offlineSlider.value = 0;
        OfflineFuel = 0;
        OfflineMetal = 0;
        OfflineCoin = 0;

        PlayerPrefs.SetFloat("OfflineMetal", OfflineMetal);
        PlayerPrefs.SetFloat("OfflineFuel", OfflineFuel);
        PlayerPrefs.SetFloat("OfflineCoin", OfflineCoin);

        OfflineAlert.SetActive(false);
    }

    public void onVolChange()
    {
        PlayerPrefs.SetFloat("volume", AudioSliderObject.GetComponent<Slider>().value);
        //Debug.Log("Volume Changed: "+ PlayerPrefs.GetFloat("volume"));
    }
    public void tempIncr()
    {
        
        tutorialTemp += 1;
    }
}
