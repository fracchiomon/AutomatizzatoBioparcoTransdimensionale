using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    Image fillableBar;
    [SerializeField] private float maxTime = 70f;
    float timeLeft;
  


    // Start is called before the first frame update
    void Start()
    {
        fillableBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
     
        timeLeft -= Time.deltaTime;
        fillableBar.fillAmount = timeLeft / maxTime;

        if (timeLeft < 0)
        {

            Debug.Log("tempo finito");

            Time.timeScale = 0;                     //quando la barra finisce termina il gioco
        }
    }
}
