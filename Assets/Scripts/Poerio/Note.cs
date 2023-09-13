using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static bool IsDebugEnabled;

    public TMPro.TextMeshProUGUI DEBUG_TEXT;
    double timeInstantiated; //timestamp relativo a quando e' istanziato l'oggetto
    public float assignedTime; //timestamp assegnato a quando "dovrebbe essere colpito"
    /*public enum NOTE_TYPE { KICK, SNARE };
    public NOTE_TYPE type;*/


    private SpriteRenderer sprite;
    public void SetColor(Color color)
    {
        sprite.color = color;
    }

    public bool CanBePressed;
    /*public KeyCode keyCode;
    public KeyCode defaultKey;*/

    private void Awake()
    {
        IsDebugEnabled = SongManager.IsDebugEnabled;
        this.sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;

        if (IsDebugEnabled)
        {
            Debug.Log(this.name + "Tempo assegnato: " + this.assignedTime);
            //DEBUG_TEXT.gameObject.SetActive(true);

        }

        /*switch (type)
        {
            case NOTE_TYPE.KICK:
                color = new Color(224, 0, 208, 255);
                sprite.color = color;
                //defaultKey = KeyCode.F;
                break;

            case NOTE_TYPE.SNARE:
                color = new Color(50, 255, 0, 255);
                sprite.color = color;
                //defaultKey = KeyCode.J;
                break;
        }*/
        timeInstantiated = SongManager.GetAudioSourceTime(); //ottengo il timeInstantiated dal tempo attuale della canzone
        if (IsDebugEnabled)
            DEBUG_TEXT.SetText($"timeInstantiated = {timeInstantiated}");

        this.CanBePressed = false;

    }

    private void Update()
    {
        //if (IsDebugEnabled)
        //    DEBUG_TEXT.rectTransform.localPosition = this.transform.localPosition;

        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated; //quanto tempo e' passato dall'istanziazione? servira' per capire quando dovrebbe arrivare a destinazione
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2)) * Time.deltaTime; //???
                                                                                                         // Utilizza Time.deltaTime per rendere il movimento fluido in base al framerate.
        float movementSpeed = (SongManager.Instance.noteSpawnY - SongManager.Instance.noteDespawnY) / (SongManager.Instance.noteTime * 2);
        float movementAmountThisFrame = movementSpeed * Time.deltaTime;

        bool checkTmoreThanOne;

        if (t > 1)
        {
            if (IsDebugEnabled)
            {
                checkTmoreThanOne = true;
                //DEBUG_TEXT.SetText($"t = {t}; t > 1 ? {checkTmoreThanOne}");

            }
            Destroy(gameObject, 1f);
        }
        else
        {
            if (IsDebugEnabled)
            {

                checkTmoreThanOne = false;
                //DEBUG_TEXT.SetText($"t = {t}; t > 1 ? {checkTmoreThanOne}");
            }

            /* OLD
                //come renderla con il Time.deltaTime?
                //transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t) ;
                //Debug.Log(Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t)) ;
                // Aggiungi il movimento fluido basato sul tempo trascorso da un frame all'altro.
             */


            transform.position -= Vector3.up * movementAmountThisFrame;
            //DEBUG_TEXT.transform.position = transform.position;
            //DEBUG_TEXT.rectTransform.position = transform.position;
            sprite.enabled = true;
        }


        /*OLD
         * if (Input.GetKeyDown(keyCode) || Input.GetKeyDown(defaultKey))
        {
            if (CanBePressed)
            {
                gameObject.SetActive(false);
            }
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            CanBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            CanBePressed = false;
        }
    }


}
