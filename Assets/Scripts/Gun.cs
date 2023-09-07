using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Texture2D CrossHair;

    [SerializeField] private Camera camera;
    [SerializeField] private float MuzzleFlashSpeed;
    private float bulletSpeed = 10;

    public float offset;
    public float rotX;
    public float rotY;
    public float rotZ;

    Vector3 pos;

    private float speed = 1f;


    private void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.x += 0.3f;
        mouseWorldPosition.y -= 0.3f;
        mouseWorldPosition.z = 5.0f;
        Cursor.SetCursor(CrossHair, (Vector2)mouseWorldPosition, CursorMode.Auto);


        Debug.Log(transform.position); 
        
        Vector3 difference = Camera.main.ScreenToWorldPoint(pos) - transform.position;


        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            var bullet = Instantiate(bulletPrefab, mouseWorldPosition, Quaternion.Euler(0,90,0),this.transform);
            Debug.Log(mouseWorldPosition);
            bullet.SetActive(true);

            //parte aggiunta
           // bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
           // bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

            Destroy(bullet, MuzzleFlashSpeed);
        }
    }

}
