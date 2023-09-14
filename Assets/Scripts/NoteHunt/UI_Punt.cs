using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UI_Punt : MonoBehaviour
{
    public Text scoreText;
    public Text messageText;
    private static int currentScore = 0;
   // private string currentMessage1 = "HAI PERSO";
    //private string currentMessage2 = "HAI VINTO";

    public int score;

    // Start is called before the first frame update
    static void Start()
    {
        UpdateScore(currentScore);                                   //inizializzo il punteggio iniziale a 0
    }

    public static void UpdateScore(int addedValue)
    {
        currentScore += addedValue;                                 //per aggiornare il punteggio ogni volta che la nota viene distrutta = +5 punteggio
        Debug.Log("Score: " + currentScore);
        //scoreText.text = " " + currentScore;
    }

    public void UpdateMessage()
    {
        //scoreText = GameObject.FindGameObjectsWithTag("ScoreText").GetComponent<Text>();
        // Text text = .GetComponent<Text>();
        // scoreText = text;

       /* if (currentScore < 10)
        {
            
            score = currentScore;
            scoreText.text = "Score: " + currentScore.ToString();
            //currentMessage1 = message;
            messageText.text = currentMessage1;
            //messageText.GetComponent<TextMeshPro>().text = currentMessage1;

        }
        else
        {
            //messageText.text = message;
            messageText.text = currentMessage2;
            //messageText.GetComponent<Text>().text = currentMessage2;
        }*/
    }




}
