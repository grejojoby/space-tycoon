using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SPSGameManager: MonoBehaviour
{
    // Start is called before the first frame update
    public int count;
    public int choice;
    public int BOTchoice;
    public bool flagStone, flagPaper, flagScissors;
    public Button buttonStone, buttonPaper, buttonScissors;
    public TextMeshProUGUI BotOutputText,resultText,yourScoreText,botScoreText;
    string op = "";
    string result = "";
    public static int you, bot;

    public GameObject gameOverPanel;
    //public static string temp;
    public enum Chc { stone=0,paper=1,scissors=2};
    void Start()
    {
        you = bot = 0;
        count = 3;
        flagPaper = flagScissors = flagStone = true;
        BotOutputText.text = "";
        resultText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        BotOutputText.text = op;
        resultText.text = result;
        yourScoreText.text = "YOU: "+you.ToString();
        botScoreText.text = "BOT: "+bot.ToString();
        // checkgameover();
        if (count == 0 || bot == 2 || you==2)
        {
            Invoke("SceneChanger", 1.5f);
            //SceneManager.LoadScene("GameOver");
        }
    }
    public void SceneChanger()
    {
        gameOverPanel.SetActive(true);
        //SceneManager.LoadScene("GameOver");
    }

    /*public void checkgameover()
    {
        if (count == 0)
        {
            buttonStone.interactable = false;
            buttonPaper.interactable = false;
            buttonScissors.interactable = false;
        }
    }*/

    public void OnbuttonClick(Button btn)
    {
        count--;
        if (btn.name == "stone")
            choice = 0;
        else if (btn.name == "paper")
            choice = 1;
        else if (btn.name == "scissors")
            choice = 2;
        BOTchoice= BOTgenerate();
        Invoke("showBotOutput",0.5f);
        Invoke("getResult", 1f);
    }

    public void ShowBotOutput()
    {
       op= System.Enum.GetName(typeof(Chc), BOTchoice);
    }

    public void GetResult()
    {
        if (choice == BOTchoice)
        { 
            result = "It is a Draw!"; 
        }
        else if (System.Enum.GetName(typeof(Chc), choice) == "stone" && System.Enum.GetName(typeof(Chc), BOTchoice) == "scissors")
        {
            result = "You win!!";
            you++;
        }
        else if (System.Enum.GetName(typeof(Chc), choice) == "paper" && System.Enum.GetName(typeof(Chc), BOTchoice) == "stone")
        { 
            result = "You win!!";
            you++;
        }
        else if (System.Enum.GetName(typeof(Chc), choice) == "scissors" && System.Enum.GetName(typeof(Chc), BOTchoice) == "paper")
        {
            result = "You win!!";
            you++;
        }
        else
        { 
            result = "You Lose...";
            bot++;
        }

    }

    public int BOTgenerate()
    {
        int rnd = Random.Range(0, 3);
        return rnd;
    }
}
