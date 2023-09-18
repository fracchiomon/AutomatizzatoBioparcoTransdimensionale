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
        GiocoManager.Instance.ToMainMenu();
    }

}
