using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        SaveManager.Instance.Load();
    }

    //funzione che stampa i punteggi da collegare ad un bottone
}
