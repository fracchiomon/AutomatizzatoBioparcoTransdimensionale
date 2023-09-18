using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameTimer : MonoBehaviour
{

    Image fillableBar;
    [SerializeField] private float maxTime ;
    private int punteggioTot;
    [SerializeField] private int punteggioVincita;
    private UI_Punt punteggioFinale;


    private static float timeLeft;
  


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

     
        Debug.Log(UI_Punt.Punteggio());
      
        if (timeLeft < 0 && UI_Punt.Punteggio()<punteggioVincita)
        {

            Debug.Log("tempo finito");

            Time.timeScale = 0;                     //quando la barra finisce termina il gioco

            SceneManager.LoadScene(sceneName: "Lose");
        }
        else if (UI_Punt.Punteggio() > punteggioVincita)
        {
            SceneManager.LoadScene(sceneName: "Victory");
        }

    }


}
