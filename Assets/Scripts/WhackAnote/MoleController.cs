using System;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private GameObject randNote;
    [SerializeField] private String[] Notes;
    private AudioSource bonk;
    public Transform[] Points;
    public int _indexPoint = 0;
    private int _indexNote = 0;
    private int contNote = 0;
    private int point;
    private float timeDelay;
    private float randomNum;
    private int randIndex;
    public Boolean hasFinished;
    public Boolean isHitted;


    // Start is called before the first frame update
    void Start()
    {
        //this.anim = GetComponent<Animator>();
        //this.anim.SetTrigger("idle");
        this.hasFinished = false;
        this.isHitted = false;
        this.point = 10;
        this.bonk = GetComponent<AudioSource>();
        randIndex = RandomNote.GetRandomIndex();
        this.timeDelay = 1.5f;
        //Debug.Log(RandomNote.GetRandomIndex() + " kk ");
    }

    public void Move()
    {
        //randIndex = RandomNote.GetRandomIndex();
        hasFinished = false;
        if (_indexPoint < this.Points.Length)
        {
            if (_indexPoint == 0 && contNote == 0)
            {
                randomNum = UnityEngine.Random.value;
                if (randomNum <= 0.67f)
                {
                    _indexNote = randIndex;
                }
                else
                {
                    _indexNote = UnityEngine.Random.Range(0, this.Notes.Length - 1);
                    while (_indexNote == randIndex)
                    {
                        _indexNote = UnityEngine.Random.Range(0, this.Notes.Length - 1);
                    }
                }
                contNote++;
            }
            else if (_indexPoint != 0)
            {
                contNote = 0;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.Points[_indexPoint].transform.position, this.MoveSpeed * Time.deltaTime);
            if (this.transform.position == this.Points[_indexPoint].transform.position && timeDelay < 0)
            {
                timeDelay = 1.5f;
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
            hasFinished = true;
        }

    }

    public int GetIndexNote()
    {
        return _indexNote;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "martello")
        {
            this._indexPoint++;
            this.isHitted = true;
            this.bonk.Play();
        }
    }

    public int GetPoint()
    {
        return this.point;
    }
}
