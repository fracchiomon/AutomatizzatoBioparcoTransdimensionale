using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Punt : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private static int currentScore;


     void Start()
    {
        currentScore = 0;
        //UpdateScore(currentScore);                                   //inizializzo il punteggio iniziale a 0
        scoreText.text = currentScore.ToString() + " PUNTI";
    }

    public static void UpdateScore(int addedValue)
    {
        currentScore += addedValue;                                 //per aggiornare il punteggio ogni volta che la nota viene distrutta = +5 punteggio
        Debug.Log("Score: " + currentScore);
        //scoreText.text = " " + currentScore;
    }

    public void Update()
    {
        scoreText.text = currentScore.ToString() + " PUNTI";
    }

    public static int Punteggio()
    {

        //    if (currentScore > 15)
        //    {
        //        SceneManager.LoadScene(sceneName: "Victory");

        //    }
        //    else
        //    {
        //        SceneManager.LoadScene(sceneName: "Lose");
        //    }
        return currentScore;
    }
}
