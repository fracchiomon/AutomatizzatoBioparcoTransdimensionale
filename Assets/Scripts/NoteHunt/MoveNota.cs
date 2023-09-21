using UnityEngine;
using System;

public class MoveNota : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject fillableBar;
    [SerializeField] private UI_BarNote Bar;
    [SerializeField] private GameObject note;                               //sono le note che si generano ogni volta che si distrugge una
    public Transform[] Points;
    [SerializeField] private string[] notePoints;
    private int score = 5;                                                  //score di ogni volta che la nota viene colpita

    [SerializeField] private AudioClip shootSFX;                            //clip audio dello sparo
    private AudioSource shootSFXSource;                                     //source creata nell'object che contiene la clip

    public GameObject ways;                                                 //2 punti per il movimento della nota
    private int _indexPoint;
    private float timeDelay;

    private bool colpito;

    private Action cambioNota;
    private GameTimer gT;

    private void Awake()
    {
        shootSFXSource = this.gameObject.AddComponent<AudioSource>();                  //creazione componente e inizializzazione parametri
        shootSFXSource.priority = 1;
        shootSFXSource.volume = 0.6f;
    }

    private void Start()
    {
        this._indexPoint = 0;
        this.timeDelay = 0.5f;
        this.colpito = false;
        this.cambioNota = FindObjectOfType<UI_BarNote>().cambioNota;
        this.gT = FindObjectOfType<GameTimer>();

        shootSFXSource.clip = shootSFX;                                      //assegno clip audio a componente
        
        
    }

    private void Update()                  
    {
        this.cambioNota();

        if (_indexPoint < this.Points.Length)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.Points[_indexPoint].transform.position, speed * Time.fixedDeltaTime);
            if (this.transform.position == this.Points[_indexPoint].transform.position && timeDelay < 0)
            {
                timeDelay = 0.5f;
                this._indexPoint++;
            }
            else
            {
                timeDelay -= Time.deltaTime;
            }
        }
        else
        {
            _indexPoint = 0;
        }
        if (colpito == true)
        {
            if (this.notePoints[_indexPoint] == Bar.GetComponent<UI_BarNote>().getNotaSelezionata().tag)
            {
                this.gT.UpdateScore(score);             //per lo score

                

                note.SetActive(true);   
                this.transform.parent.gameObject.SetActive(false);
            }
            colpito = false;

        }
        this.gT.GameTimerUpdate();
    }


    void OnMouseDown()
    {
        colpito = true;                     // Destroy the gameObject after clicking on it
                                            //riproduzione suono sparo
        if (shootSFXSource != null)
            shootSFXSource.Play();
    }

}
