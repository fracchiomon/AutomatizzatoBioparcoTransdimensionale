using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameCanvas : MonoBehaviour
{
    [SerializeField] private Button _cancelBtn, _quitButton;
    private Canvas _songSelectCanvas;
    void Start()
    {
        try
        {

            _songSelectCanvas = FindObjectOfType<SongSelectCanvas>(includeInactive: true).GetComponent<Canvas>();
        }
        catch
        {
            print(new NullReferenceException().Message);
        }
    }

    public void RHYTHMICON_CancelSafe() //se non c'è audio caricato, quando annullo l'azione di ritorno al menu, non rimane a pannello vuoto ma riprende la schermata di selezione canzone
    {
        if (SongManager.Instance.audioSource == null)
        {
            _songSelectCanvas.enabled = true;
        }
        else
        {
            SongManager.Instance.StartSong();
        }
    }

}
