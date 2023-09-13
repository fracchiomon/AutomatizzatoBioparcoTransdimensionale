using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_showButton : MonoBehaviour
{
    [SerializeField] private GameObject toShow;
    [SerializeField] private TextMeshProUGUI btnText;

    public void OnShowClicked()
    {
        if(this.btnText.text.Equals("Show"))
        {
            this.toShow.SetActive(true);
            this.btnText.text = "Hide";
        }
        else if(this.btnText.text.Equals("Hide"))
        {
            this.toShow.SetActive(false);
            this.btnText.text = "Show";
        }
    }
}
