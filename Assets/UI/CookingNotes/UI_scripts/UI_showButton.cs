using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_showButton : MonoBehaviour
{
    public enum BTN_STATE
    {
        HIDE,
        SHOW
    }

    [SerializeField] private GameObject toShow;
    [SerializeField] private TextMeshProUGUI btnText;
    [SerializeField] private BTN_STATE state;
    [SerializeField] private string strOnShow;
    [SerializeField] private string strOnHide;

    private void Start()
    {
        if(this.state == BTN_STATE.SHOW)
        {
            this.btnText.text = this.strOnShow;
        }
    }

    public void OnShowClicked()
    {
        if(this.state == BTN_STATE.SHOW)
        {
            this.toShow.SetActive(true);
            this.btnText.text = this.strOnHide;
            this.state = BTN_STATE.HIDE;
        }
        else 
        {
            this.toShow.SetActive(false);
            this.btnText.text = this.strOnShow;
            this.state = BTN_STATE.SHOW;
        }
    }
}
