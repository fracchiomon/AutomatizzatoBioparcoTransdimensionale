using UnityEngine;


public class MoveNota : MonoBehaviour
{
    [SerializeField] private float speed = 5;                                 //velocit� nota

    [SerializeField] private UI_BarNote Bar;
    [SerializeField] private GameObject note;    //sono le note che si generano ogni volta che si distrugge una
    public Transform[] Points;
    [SerializeField] private string[] notePoints;
    private int score = 5;                              //score di ogni volta che la nota viene colpita


    public GameObject ways;                             //2 punti per il movimento della nota
    private int _indexPoint;
    private float timeDelay;

    private bool colpito;

    private GameTimer gT; 

    private void Start()
    {
        this._indexPoint = 0;
        this.timeDelay = 0.5f;
        this.colpito = false;
    }

    private void FixedUpdate()                  
    {
 
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
                GameTimer.UpdateScore(score);             //per lo score
                note.SetActive(true);                   //serve per generare la nuova nota dopo che e' stata distrutta
                this.transform.parent.gameObject.SetActive(false);
            }
            colpito = false;

        }
    }

    public void setColpito()
    {
        colpito = true;
    }


    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        colpito = true;
    }

}
