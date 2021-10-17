using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
   // public GameObject resetpanel;
    public void Resetall(GameObject resetpanel)
    {
        PlayerPrefs.DeleteAll();
        resetpanel.SetActive(true);
    }
}
