using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_MenuPunteggi : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textRythmicon;
    [SerializeField] private TextMeshProUGUI textNoteHunt;
    [SerializeField] private TextMeshProUGUI textFindTheNote;
    [SerializeField] private TextMeshProUGUI textCookingNotes;
    [SerializeField] private TextMeshProUGUI textWhackANote;

    void Start()
    {
        this.textRythmicon.text = (SaveManager.Instance.bestRythmicon).ToString();
        this.textNoteHunt.text = (SaveManager.Instance.bestNoteHunt).ToString();
        this.textFindTheNote.text = (SaveManager.Instance.bestFindTheNote).ToString();
        this.textCookingNotes.text = (SaveManager.Instance.bestCookingNotes).ToString();
        this.textWhackANote.text = (SaveManager.Instance.bestWhackANote).ToString();
    }

}
