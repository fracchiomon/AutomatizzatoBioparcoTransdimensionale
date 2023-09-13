using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_Sounds : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip hoverEFX, clickEFX;
    private Button thisBtn;
    private AudioSource hoverAudio, clickAudio;

    void Awake()
    {
        thisBtn = GetComponent<Button>();
        hoverAudio = this.gameObject.AddComponent<AudioSource>();
        clickAudio = this.gameObject.AddComponent<AudioSource>();

        hoverAudio.playOnAwake = clickAudio.playOnAwake = false;
        hoverAudio.priority = clickAudio.priority = 1;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEFX != null)
        {
            hoverAudio.clip = hoverEFX;
            hoverAudio.PlayOneShot(hoverEFX);
        }
    }

    public void ButtonClicked()
    {
        if (clickEFX != null)
        {
            clickAudio.clip = clickEFX;
            clickAudio.PlayOneShot(clickEFX);
        }
    }

}
