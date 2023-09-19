using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        SaveManager.Instance.Load();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log( SaveManager.Instance.bestCookingNotes + " " + SaveManager.Instance.bestRythmicon);
        }
    }

    //funzione che stampa i punteggi da collegare ad un bottone
}
