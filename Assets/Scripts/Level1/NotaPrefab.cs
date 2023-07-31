using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaPrefab : MonoBehaviour
{
    public bool IsInteractable { get; private set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsInteractable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsInteractable = false;
    }
}
