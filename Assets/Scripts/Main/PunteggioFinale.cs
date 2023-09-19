using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunteggioFinale : MonoBehaviour
{
    [SerializeField] private ScoreForMiniGame finalScore;

    [SerializeField] private Text screenScore;

    private void Start()
    {
      
        screenScore.text = "Your score is "+finalScore.GetHighScore().ToString(); 
    }
}
