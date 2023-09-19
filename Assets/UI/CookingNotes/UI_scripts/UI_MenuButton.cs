using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_MenuButton : MonoBehaviour
{
    public void ToMainMenu()
    {
        this.GetComponent<Button>().interactable = false;
        if (SongManager.Instance != null) SongManager.Instance.StopSong();
        GiocoManager.Instance.ToMainMenu();
    }

}
