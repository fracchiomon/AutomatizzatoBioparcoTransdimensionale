using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    //[SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Texture2D CrossHair;

    //[SerializeField] private Camera camera;                             
    [SerializeField] private float MuzzleFlashSpeed;                    //fiamma sparata velocità

    Vector3 pos;
   // Vector3 prevMousePos;
    Vector3 mouseWorldPosition;



    private void Update()
    {
        pos = Input.mousePosition;
        Cursor.SetCursor(CrossHair, (Vector2)pos, CursorMode.Auto);

        Vector3 difference = Camera.main.ScreenToWorldPoint(pos) - transform.position;


        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 5.0f;

            var bullet = Instantiate(bulletPrefab, mouseWorldPosition, Quaternion.Euler(0,90,0), this.transform);
            bullet.SetActive(true);

            //raycast per sparo
            //pos.z = speed;
            transform.position = Camera.main.ScreenToWorldPoint(pos);


            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Nota") && hit.collider.CompareTag("Menu"))
            {
                Debug.Log("RAYCAST");

                hit.collider.GetComponent<MoveNota>().setColpito();
            }

            Destroy(bullet, MuzzleFlashSpeed);
 

        }
    }

}
