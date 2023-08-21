using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class HammerController : MonoBehaviour
{

    //Vector3 mos_pos;

    //Vector3 mos_world_pos;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private float moveSpeed = 4.0f; // Velocità di movimento dell'oggetto
    private void Update()
    {
        // Verifica se il tasto D è premuto
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calcola il vettore di movimento
        Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

        // Applica il movimento all'oggetto
        transform.Translate(movement);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //mos_pos = Mouse.current.position.ReadValue();
            //mos_pos.z = Camera.main.nearClipPlane * 3f + 1;
            //mos_world_pos = Camera.main.ScreenToWorldPoint(mos_pos);
            //this.transform.position = mos_world_pos;
            anim.SetTrigger("colpo");
        }
        else
            anim.SetTrigger("default");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("mole"))
        {
            anim.SetTrigger("default");
        }
    }
}
