using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnitySystem.Random;
using TMPro; // serve per il testo UI 
using UnityEngine.UI;       //per interagire con la UI
using System;

public class UI : MonoBehaviour
{

    Image fillableBar;
    public float maxTime = 70f;
    public float noteTime = 5f;
    float timeLeft;
    public GameObject gameOverText;
    

    public GameObject Do;
    public GameObject Re;
    public GameObject Mi;
    public GameObject Fa;
    public GameObject Sol;
    public GameObject La;
    public GameObject Si;

   
   
    void Start()
    {
        gameOverText.SetActive(false);
        fillableBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    private void Update()
    {

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            fillableBar.fillAmount = timeLeft / maxTime;

            Re.SetActive(false);
            Mi.SetActive(false);
            Fa.SetActive(false);
            Sol.SetActive(false);
            La.SetActive(false);
            Si.SetActive(false);


            if (timeLeft > 42)
                {
                    Do.SetActive(true);
            }
                else if (timeLeft > 35)
                {
                    Do.SetActive(false);
                    Re.SetActive(true);
            }
                else if (timeLeft > 28) 
                {
                    Re.SetActive(false);
                    Mi.SetActive(true);
                }
            else if (timeLeft >21)
            {
                Mi.SetActive(false);
                Fa.SetActive(true);
            }
            else if (timeLeft > 14)
            {
                Fa.SetActive(false);
                Sol.SetActive(true);
            }
            else if (timeLeft > 7)
            {
                Sol.SetActive(false);
                La.SetActive(true);
            }
            else if (timeLeft > 0)
            {
                La.SetActive(false);
                Si.SetActive(true);
            }

        }
        else
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;
            
        }


    }

    private void Awake()
    {

    }

    /* private void Note()
     {
         int note = new System.Random().Next(1, 7);
         switch (note)
         {
             case 1:
                 Console.WriteLine("Do");
                 Do.SetActive(true);
                 break;

             case 2:
                 Re.SetActive(true);
                 break;

             case 3:
                 Mi.SetActive(true);
                 break;

             case 4:
                 Fa.SetActive(true);
                 break;

             case 5:
                 Sol.SetActive(true);
                 break;

             case 6:
                 La.SetActive(true);
                 break;

             case 7:
                 Si.SetActive(true);
                 break;

             default:
                 break;

         }
     }*/



    //-------------------------------------------------------------------------------------------------
    //Non funziona va subito al game over
    /*for (int num = 0; num < 4; num++) 
    {
        gameOverText.SetActive(false);
        if (timeLeft > 0)
        {

            timeLeft -= Time.deltaTime;
            fillableBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            fillableBar = GetComponent<Image>();
            timeLeft = maxTime;
            //gameOverText.SetActive(true);
            //Time.timeScale = 0;
        }
    }


        gameOverText.SetActive(true);
        Time.timeScale = 0;*/

    //----------------------------------------------------------------------------------------------

    //Funziona per ogni volta far ripartire la barra
    /*if (timeLeft > 0)
    {
        timeLeft -= Time.deltaTime;
        fillableBar.fillAmount = timeLeft / maxTime;
    }
    else
    {
        fillableBar = GetComponent<Image>();
        timeLeft = maxTime;
        //gameOverText.SetActive(true);
        //Time.timeScale = 0;
    }*/





}
