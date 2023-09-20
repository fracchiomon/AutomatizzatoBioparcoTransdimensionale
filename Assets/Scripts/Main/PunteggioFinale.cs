using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PunteggioFinale : MonoBehaviour
{
    //[SerializeField] private ScoreForMiniGame finalScore;

    [SerializeField] private TextMeshProUGUI screenScore;

    private void Start()
    {
        this.screenScore.text = "Your score is " + ScoreForMiniGame.Instance.GetHighScore().ToString();
        //screenScore.text = "Your score is "+ ScoreForMiniGame.Instance.GetHighScore().ToString(); 
    }
}
