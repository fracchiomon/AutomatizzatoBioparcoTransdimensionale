
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_LegendItem : MonoBehaviour
{
    private SO_NotaItem _data;
    private UI_Legend controller;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDurationText;

    public SO_NotaItem data => this._data;

    public void Setup(UI_Legend controller, SO_NotaItem data)
    {
        this.controller = controller;
        this._data = data;
        UpdateGraphics();
    }

    public void UpdateGraphics()
    {
        this.itemIcon.sprite = this.data.icon;
        this.itemNameText.text = this.data.type;
        this.itemDurationText.text = this.data.stringDurata;
    }
}
