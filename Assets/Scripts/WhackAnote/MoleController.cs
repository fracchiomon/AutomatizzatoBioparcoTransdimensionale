using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
    private float point;
    private float timeDelay = 2.0f;
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
        this.point = 10.0f;
        this.bonk = GetComponent<AudioSource>();
    }

    public void Move()
    {
        randIndex = RandomNote.GetRandomIndex();
        hasFinished = false;
        if (_indexPoint < this.Points.Length)
        {
            if (_indexPoint == 0 && contNote == 0)
            {
                randomNum = UnityEngine.Random.value;
                if (randomNum <= 0.5f)
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
            Debug.Log(Notes[_indexNote]);
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.Points[_indexPoint].transform.position, this.MoveSpeed * Time.deltaTime);
            if (this.transform.position == this.Points[_indexPoint].transform.position && timeDelay < 0)
            {
                timeDelay = 2.0f;
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

    public float GetPoint()
    {
        return this.point;
    }
}
