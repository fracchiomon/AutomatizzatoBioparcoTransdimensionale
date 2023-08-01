using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated; //timestamp relativo a quando e' istanziato l'oggetto
    public float assignedTime; //timestamp assegnato a quando "dovrebbe essere colpito"
    public enum NOTE_TYPE { KICK, SNARE };
    public NOTE_TYPE type;
    private SpriteRenderer sprite;
    private Color color;

    /*public bool CanBePressed { get; private set; }
    public KeyCode keyCode;
    public KeyCode defaultKey;*/

    private void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();


        switch (type)
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
        }
        timeInstantiated = SongManager.GetAudioSourceTime(); //ottengo il timeInstantiated dal tempo attuale della canzone
    }

    private void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated; //quanto tempo e' passato dall'istanziazione? servira' per capire quando dovrebbe arrivare a destinazione
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2)); //???


        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t) ;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        /*if (Input.GetKeyDown(keyCode) || Input.GetKeyDown(defaultKey))
        {
            if (CanBePressed)
            {
                gameObject.SetActive(false);
            }
        }*/
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            CanBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            CanBePressed = false;
        }
    }*/

    
}
