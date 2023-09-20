
using UnityEngine;
using TMPro;
using System;

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
    [SerializeField] private LevelManager lvlManager;
    private Action<float> UpdateScore;

    private void Start()
    {
        if(this.state == BTN_STATE.SHOW)
        {
            this.btnText.text = this.strOnShow;
        }
        if(this.lvlManager == null)
        {
            this.lvlManager = FindObjectOfType<LevelManager>();
        }
        UpdateScore = this.lvlManager.ReduceScore;
    }

    private void Update()
    {
        //per ogni secondo in cui la legenda è visibile e il gioco è in play
        //viene tolto 1 punto
        if(this.toShow.gameObject.activeInHierarchy && this.lvlManager.isOnPlay)
        {
            UpdateScore(Time.deltaTime);
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
