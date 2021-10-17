using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SevenUpSevenDown : MonoBehaviour
{
    public Button playbutton;

   // void Update()
    //{
       // if (GameManager.fuel >= 1000)
         //   playbutton.interactable = true;
        //else
          //  playbutton.interactable = false;
   // }
    public LevelLoader7up ll;
    public void Play7Up()
    {
     //   GameManager.fuel -= 1000;
        ll.LoadLevel(2);
    }
}
