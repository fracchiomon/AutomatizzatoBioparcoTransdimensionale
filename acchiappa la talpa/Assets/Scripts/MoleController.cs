using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    public int _indexPoint = 0;
    [SerializeField] public Transform[] Points;
    private float timeDelay = 2.0f;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetTrigger("idle");
    }

    public void Move()
    {
        if (_indexPoint < this.Points.Length)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.Points[_indexPoint].transform.position, this.MoveSpeed * Time.deltaTime);
            if (this.transform.position == this.Points[_indexPoint].transform.position && timeDelay < 0)
            {
                timeDelay = 2.0f;
                Debug.Log(_indexPoint);
                this._indexPoint++;
            }
            else
            {
                timeDelay -= Time.deltaTime;
            }
        }
    }
}
