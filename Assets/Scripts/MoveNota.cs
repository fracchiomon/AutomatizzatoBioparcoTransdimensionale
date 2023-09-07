using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNota : MonoBehaviour
{
    /* public Rigidbody rb;

     void Update()
     {
         rb.AddForce(Vector3.up * 8);
         //rb.AddForce (Vector3.down * 8);
     }*/


    public float speed;
    Vector3 targetPos;

    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;


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
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (transform.position == targetPos) 
        {
            NextPoint();
        }
    }

    void NextPoint()
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
}
