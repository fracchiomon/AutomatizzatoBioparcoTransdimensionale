
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CompletationPanel : MonoBehaviour
{
    [SerializeField] private Image fillableBar;
    [SerializeField] private TextMeshProUGUI notes;
    [SerializeField] private TextMeshProUGUI rests;

    public void UpdateGraphics(float current, float max, string notes, string rests)
    {
        this.fillableBar.fillAmount = current / max;
        this.notes.text = notes;
        this.rests.text = rests;
    }

}
