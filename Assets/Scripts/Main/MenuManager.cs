using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //aggiunta la spunta per disattivare da inspector stampa di debug
    [SerializeField] private bool IsDebugEnabled;

    private void Start()
    {
        SaveManager.Instance.Load();
    }

    private void Update()
    {
        if (IsDebugEnabled)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log( SaveManager.Instance.bestRythmicon + " " + SaveManager.Instance.bestNoteHunt + " " +

                    SaveManager.Instance.bestFindTheNote + " " + SaveManager.Instance.bestCookingNotes + " " +
                    SaveManager.Instance.bestWhackANote

                    );
            }

        }
        //grezzo check per l'uscita dal gioco qualora non fossimo su WebGL
        if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(!(Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))) 
            {
                Application.Quit(exitCode: 0 );
            }
        }
    }

    //funzione che stampa i punteggi da collegare ad un bottone
}
