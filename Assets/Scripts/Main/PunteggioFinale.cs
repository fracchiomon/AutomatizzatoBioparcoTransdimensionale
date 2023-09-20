using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunteggioFinale : MonoBehaviour
{
     [SerializeField] private Text screenScore;

    private void Start()
    {
      
        screenScore.text = "Your score is "+ScoreForMiniGame.Instance.GetHighScore().ToString(); 
    }
}
