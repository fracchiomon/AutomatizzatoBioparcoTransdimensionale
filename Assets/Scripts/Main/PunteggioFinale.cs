using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PunteggioFinale : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI screenScore;

    private void Start()
    {

        screenScore.text = "Your score is " + ScoreForMiniGame.Instance.GetHighScore().ToString();
    }


}
