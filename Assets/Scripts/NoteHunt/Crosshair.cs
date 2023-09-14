using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Texture2D CrossHair;

    //[SerializeField] private Camera camera;                             // --- VEDERE SE RIAGGIUNGERE
    [SerializeField] private float MuzzleFlashSpeed;                    //fiamma sparata velocità
    // private float bulletSpeed = 10;--- VEDERE SE RIAGGIUNGERE

    public float offset;
    public float rotX;
    public float rotY;
    public float rotZ;

    Vector3 pos;
    Vector3 prevMousePos;
    Vector3 mouseWorldPosition;

    //private float speed = 1f;--- VEDERE SE RIAGGIUNGERE


    private void Update()
    {
        //DA AGGIUNGERE SE NON FUNZIONA IL PEZZO AGGIUNTO
        pos = Input.mousePosition;
        Cursor.SetCursor(CrossHair, (Vector2)pos, CursorMode.Auto);

        Vector3 difference = Camera.main.ScreenToWorldPoint(pos) - transform.position;

        // ---------------------------------------------------------------------------------
        //PROVA  DIREZIONE PROIETTILE

      /*  Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.x += 0f;
        mouseWorldPosition.y -= 0.3f;
        mouseWorldPosition.z = 5.0f;
        Cursor.SetCursor(CrossHair, (Vector3)mouseWorldPosition, CursorMode.Auto);


        //Debug.Log(transform.position);

        Vector3 difference = Camera.main.ScreenToWorldPoint(pos) - transform.position;*/



        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 5.0f;

            var bullet = Instantiate(bulletPrefab, mouseWorldPosition, Quaternion.Euler(0,90,0),this.transform);
            bullet.SetActive(true);

            //raycast per sparo
            //pos.z = speed;
            transform.position = Camera.main.ScreenToWorldPoint(pos);

            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Nota"))
            {

                Debug.Log("RAYCAST");
                hit.collider.GetComponent<MoveNota>().setColpito();


                //PROVA RAYCAST
                /*Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay))
                {
                    //Qualcosa è stato colpito!
                }*/
            }

            Destroy(bullet, MuzzleFlashSpeed);
        }
    }

}
