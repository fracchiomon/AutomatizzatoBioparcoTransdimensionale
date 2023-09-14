using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnitySystem.Random;
using TMPro; // serve per il testo UI 
using UnityEngine.UI;       //per interagire con la UI
using System;
using Unity.PlasticSCM.Editor.WebApi;

public class UI_FillableBar : MonoBehaviour
{

    Image fillableBar;
    [SerializeField] private float maxTime = 70f;
    float timeLeft;
    private float timeSpawn = 0;
    //[SerializeField]private UI_Punt ui;


    [SerializeField] private GameObject schermata1;
    [SerializeField] private GameObject schermata2;
    [SerializeField] private GameObject punteggio;
    [SerializeField] private Text scoreText;
   // [SerializeField] public Text ;
 //   [SerializeField] public GameObject schermata2;


    //Image schermata2;
    private int choice = 0;                     //serve per generare nuove note tramite il switch
    private bool isNext = true;                 //per cambiare la nota UI
    private int prev;                           //per non generare una nota successiva uguale nella UI

    private GameObject checkNota;

    [SerializeField] private GameObject[] note;         //array per generale note differenti

    //sono per il testo delle note
    [SerializeField] private TextMeshProUGUI testoNote;

    public TextMeshProUGUI GetTestoNote => this.testoNote;


    void Start()
    {
        //gameOverText.SetActive(false);
        fillableBar = GetComponent<Image>();
        timeLeft = maxTime;

        for (int i = 0; i < note.Length; i++)
        {
            note[i].SetActive(false);
        }
        SpawnNota();
    }

    private void Update()
    {
        this.timeSpawn += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        fillableBar.fillAmount = timeLeft / maxTime;

        if (timeLeft > 0 && this.timeSpawn >= 7f)
        {
            SpawnNota();
            this.timeSpawn = 0f;
            prev = choice;

        }
        else if(timeLeft < 0)
        {
            
            schermata1.SetActive(true);                 //scermata punteggio finale
            schermata2.SetActive(true);
            //ui.UpdateMessage();
           // punteggio.SetActive(true);

            //scoreText.SetActive(true);  

            Time.timeScale = 0;                     //quando la barra finisce termina il gioco
        }

    }


    public void SpawnNota()
    {
        choice = new System.Random().Next(0, 6);

        if (choice == prev)                          //se la nota scelta e' uguale a quella precedente ne sceglie una nuova
        {
            choice = new System.Random().Next(0, 6);
        }
        Debug.Log(choice);
        switch (choice)                                         //per assegnare le note nella UI e renderle attive
        {
            case 0: //DO
                this.testoNote.text = "Do";
                /*note[0].SetActive(true);
                isNext = false;
                StartCoroutine(NoteTime(choice));
                checkNota = note[0];*/
                break;

            case 1: //Re
                this.testoNote.text = "Re";
                /*note[1].SetActive(true);
                isNext = false;
                StartCoroutine(NoteTime(choice));
                checkNota = note[1];*/
                break;

            case 2: //Mi
                this.testoNote.text = "Mi";
                /*note[2].SetActive(true);
                isNext = false;
                StartCoroutine(NoteTime(choice));
                checkNota = note[2];*/
                break;

            case 3: //Fa
                this.testoNote.text = "Fa";
                /*note[3].SetActive(true);
                isNext = false;
                StartCoroutine(NoteTime(choice));
                checkNota = note[3];*/
                break;

            case 4: //Sol
                this.testoNote.text = "Sol";
                /*note[4].SetActive(true);
                isNext = false;
                StartCoroutine(NoteTime(choice));
                checkNota = note[4];*/
                break;

            case 5: //La
                this.testoNote.text = "La";
                /*note[5].SetActive(true);
                isNext = false;
                StartCoroutine(NoteTime(choice));
                checkNota = note[5];*/
                break;

            case 6: //Si
                this.testoNote.text = "Si";
                /* note[6].SetActive(true);
               isNext = false;
               StartCoroutine(NoteTime(choice));
               checkNota = note[6];*/
                break;
        }
    }

    /*IEnumerator NoteTime(int ch)                        //stabilisce il tempo della nota che deve essere generata nella UI
    {
        yield return new WaitForSeconds(7f);
        note[ch].SetActive(false);
        isNext = true;
    }*/

    public GameObject getNotaSelezionata()
    {
        return checkNota;
    }


}
