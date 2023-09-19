using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMOD;
using FMODUnity;

public class UI_Button_Sounds : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private StudioEventEmitter hoverEFX, clickEFX;
    private Button thisBtn;

    void Awake()
    {
        thisBtn = GetComponent<Button>();
        //hoverAudio = this.gameObject.AddComponent<AudioSource>();
        //clickAudio = this.gameObject.AddComponent<AudioSource>();

        //hoverAudio.playOnAwake = clickAudio.playOnAwake = false;
        //hoverAudio.priority = clickAudio.priority = 1;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEFX != null)
        {
            hoverEFX.Play();
        }
    }

    public void ButtonClicked()
    {
        if (clickEFX != null)
        {
            clickEFX.Play();
        }
    }

}
