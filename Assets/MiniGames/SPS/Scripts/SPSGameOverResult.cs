using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SPSGameOverResult : MonoBehaviour
{
    public TextMeshProUGUI yourFinalScore,botFinalScore,finalScore;
    //public static gamemanager gm;

    // Update is called once per frame
    void Update()
    {
        Display();
    }
    public void Display()
    {
        yourFinalScore.text="You: "+SPSGameManager.you.ToString();
        botFinalScore.text = "BOT: "+ SPSGameManager.bot.ToString();
        if (SPSGameManager.bot > SPSGameManager.you)
            finalScore.text = "YOU LOSE";
        else if (SPSGameManager.you > SPSGameManager.bot)
            finalScore.text = "YOU  WIN";
        else
            finalScore.text = "Its a DRAW";
    }
}
