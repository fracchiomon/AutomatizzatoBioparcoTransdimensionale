using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life;
    [SerializeField] private GameObject EnemyNota;
    //public GameObject note;

    Vector3 pos;

    public float speed = 1f;

    void Update()
    {
        pos = Input.mousePosition;
        pos.z = speed;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {

        Destroy(collision.gameObject);
        Destroy(gameObject);

        /*Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit; 

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.name == "EnemyNote") 
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
        }*/

    }

    /*void AudioSourceCreation()
     {
         audioSource = GetComponent(); //Se già esisteva usa quello esistente
     }*/
}
