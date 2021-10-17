using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public GameObject AudioSourceObject;
    public Slider AudioSliderObject;
    // Start is called before the first frame update
    void Start()
    {
        AudioSourceObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume", 1f);
        AudioSliderObject.value = PlayerPrefs.GetFloat("volume", 1f);
        Debug.Log("vol: "+PlayerPrefs.GetFloat("volume", 1f));
    }

    
    public void onVolChange()
    {
        PlayerPrefs.SetFloat("volume", AudioSliderObject.GetComponent<Slider>().value);
        Debug.Log("Volume Changed: "+ PlayerPrefs.GetFloat("volume"));
    }
}
