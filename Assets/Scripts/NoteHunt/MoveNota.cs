using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class MoveNota : MonoBehaviour
{
    public float speed;                                 //velocit� nota
    Vector3 targetPos;
    //[SerializeField]private UI_Punt;

    //[SerializeField] private UI_FillableBar Bar;
    [SerializeField] private GameObject Bar;

    [SerializeField] private GameObject note;           //sono le note che si generano ogni volta che si distrugge una

    private int score = 5;                              //score di ogni volta che la nota viene colpita


    public GameObject ways;                             //2 punti per il movimento della nota
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    public bool colpito = false;



    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for ( int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    private void FixedUpdate()                  
    {
        //movimento nota
        var step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (transform.position == targetPos) 
        {
            NextPoint();
        }

    }


    void NextPoint()                        //per far ritornare avanti e indietro la nota
    {
       if (pointIndex == pointCount -1)
        {
            direction = -1;
        }

       if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
    }


    public void setColpito()
    {
        colpito = true;
    }

    public void OnTriggerEnter(Collider other)
    {

        //quando viene colpita la nota nella posizione giusta
        if(colpito == true)
        {
            Debug.Log(other.tag + "=othertag   bar=" + Bar.GetComponent<UI_BarNote>().getNotaSelezionata().tag);
            Debug.Log("Other tag=" + other.tag);
            Debug.Log("Bar=" + Bar.GetComponent<UI_BarNote>().getNotaSelezionata().tag);
            if (other.tag == Bar.GetComponent<UI_BarNote>().getNotaSelezionata().tag)           //bar � per le note UI
            {
                Debug.Log("Other tag: " + other.tag);
                //Destroy(transform.parent.gameObject);


                UI_Punt.UpdateScore(score);             //per lo score

                //Instantiate(note, transform.position, Quaternion.identity);
                note.SetActive(true);                   //serve per generare la nuova nota dopo che e' stata distrutta
                transform.parent.gameObject.SetActive(false);

               // Destroy(this.gameObject);
            }

            colpito = false;
            
        }

    }

}
