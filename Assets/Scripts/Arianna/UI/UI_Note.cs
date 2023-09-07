using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Note : MonoBehaviour
{
   public float maxTime = 5f;
    float timeLeft;
    //public GameObject gameOverText;
    TextMeshPro notaSol;

    void Start()
    {
        //notaSol.SetActive(true);
        //fillableBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    private void Update()
    {
        if (timeLeft > 12)
        {
            //notaSol;
            //timeLeft -= Time.deltaTime;
            //fillableBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            //notaSol.SetActive(false);
            //gameOverText.SetActive(true);
            //Time.timeScale = 0;
        }
    }

}
