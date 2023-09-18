using System.Collections;

using UnityEngine;


public class UI_BarNote : MonoBehaviour
{

    //Image fillableBar;
    //[SerializeField] private float maxTime = 70f;
    float TimeLeft;
    // private float timeSpawn = 0;

 

    //[SerializeField] private Text scoreText;


    private int choice = 0;                     //serve per generare nuove note tramite il switch
    private bool isNext = true;                 //per cambiare la nota UI
    private int prev;                           //per non generare una nota successiva uguale nella UI

    private GameObject checkNota;

    [SerializeField] private GameObject[] note;         //array per generale note differenti


    void Start()
    {
        //fillableBar = GetComponent<Image>();
        // timeLeft = maxTime;
        
        for (int i = 0; i < note.Length; i++)
        {
            note[i].SetActive(false);
        }
    }

    private void Update()
    {

        //  timeLeft -= Time.deltaTime;
        // fillableBar.fillAmount = timeLeft / maxTime;

        
        if (/*TimeLeft > 0 &&*/ isNext)
        {
            
            choice = new System.Random().Next(0, 6);

            if (choice == prev)                          //se la nota scelta e' uguale a quella precedente ne sceglie una nuova
            {
                choice = new System.Random().Next(0, 6);
            }

            switch (choice)                                         //per assegnare le note nella UI e renderle attive
            {
                case 0: //DO
                    note[0].SetActive(true);
                    isNext = false;
                    StartCoroutine(NoteTime(choice));
                    checkNota = note[0];
                    break;

                case 1: //Re
                    note[1].SetActive(true);
                    isNext = false;
                    StartCoroutine(NoteTime(choice));
                    checkNota = note[1];
                    break;

                case 2: //Mi
                    note[2].SetActive(true);
                    isNext = false;
                    StartCoroutine(NoteTime(choice));
                    checkNota = note[2];
                    break;

                case 3: //Fa
                    note[3].SetActive(true);
                    isNext = false;
                    StartCoroutine(NoteTime(choice));
                    checkNota = note[3];
                    break;

                case 4: //Sol
                    note[4].SetActive(true);
                    isNext = false;
                    StartCoroutine(NoteTime(choice));
                    checkNota = note[4];
                    break;

                case 5: //La
                    note[5].SetActive(true);
                    isNext = false;
                    StartCoroutine(NoteTime(choice));
                    checkNota = note[5];
                    break;

                case 6: //Si
                    note[6].SetActive(true);
                    isNext = false;
                    StartCoroutine(NoteTime(choice));
                    checkNota = note[6];
                    break;
            }
            prev = choice;

        }
        //else
        //{
        //    UI_Punt.Punteggio();
        //}
        
        //else /*if (TimeLeft < 0)*/
        //{
        //    UI_Punt.Punteggio();

        //    //Time.timeScale = 0;                     //quando la barra finisce termina il gioco
        //}

    }

    IEnumerator NoteTime(int ch)                        //stabilisce il tempo della nota che deve essere generata nella UI
    {
        yield return new WaitForSeconds(7f);
        note[ch].SetActive(false);
        isNext = true;
    }

    public GameObject getNotaSelezionata()
    {
        return checkNota;
    }


}

