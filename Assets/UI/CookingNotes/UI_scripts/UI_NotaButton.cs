using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_NotaButton : MonoBehaviour
{
    //collegare la componente agli scriptable object
    private SO_NotaItem _data;
    private UI_windowDispensa controller;
    [SerializeField] private Button thisBtn;
    [SerializeField] private Image itemIcon;

    public SO_NotaItem data => this._data;
    public Button button => this.thisBtn;

    //la funzione setta lo scriptable obj associato al bottone
    //e aggiorna la grafica
    public void Setup(UI_windowDispensa controller, SO_NotaItem data)
    {
        if (this.thisBtn == null)
        {
            this.thisBtn = this.GetComponent<Button>();
        }
        this.controller = controller;
        this._data = data;
        UpdateGraphics();
    }

    //invece dello start ho una funzione che aggiorna la grafica
    //del bottone quando necessario
    public void UpdateGraphics()
    {
        if (this.data == null)
        {
            this.thisBtn.interactable = false;
            this.itemIcon.gameObject.SetActive(false);
        }
        else
        {
            this.thisBtn.interactable = true;
            this.itemIcon.sprite = this.data.icon;
            this.itemIcon.gameObject.SetActive(true);
        }
    }

    //quando clicco sul bottone viene avvertito il controller che è stato cliccato
    //lui
    public void OnClick()
    {
        this.controller.OnSelectedItem(this);
    }
}
