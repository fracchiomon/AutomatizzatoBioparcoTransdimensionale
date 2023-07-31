using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum NOTE_TYPE { KICK, SNARE};
    public NOTE_TYPE type;
    private SpriteRenderer sprite;
    private Color color;

    public bool CanBePressed { get; private set; }
    public KeyCode keyCode;
    public KeyCode defaultKey;

    private void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        
        
        switch(type)
        {
            case NOTE_TYPE.KICK:
                color = new Color(224, 0, 208, 255);
                sprite.color = color;
                defaultKey = KeyCode.F;
                break;
            
            case NOTE_TYPE.SNARE:
                color = new Color(50, 255, 0, 255);
                sprite.color = color;
                defaultKey = KeyCode.J;
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode) || Input.GetKeyDown(defaultKey))
        {
            if(CanBePressed)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.tag == "Activator")
        {
            CanBePressed = true;
        }
    }
    private void OnTriggerExit2D( Collider2D collision )
    {
        if (collision.tag == "Activator")
        {
            CanBePressed = false;
        }
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
