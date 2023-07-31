using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaPrefab : MonoBehaviour
{
    private Level1_Control lvl1;

    public enum NOTE_TYPE { KICK, SNARE };
    public NOTE_TYPE type;
    private SpriteRenderer sprite;
    private Color color;

    public bool CanBePressed { get; private set; }

    public KeyCode keyCode;
    //private GameManager gameManager;

    private void Start()
    {
        lvl1 = Level1_Control.GetInstance();
        this.sprite = GetComponent<SpriteRenderer>();
        //this.gameManager = GameManager.GetInstance();

        switch (type)
        {
            case NOTE_TYPE.KICK:
                color = Color.red;
                sprite.color = color;
                keyCode = KeyCode.F;
                break;

            case NOTE_TYPE.SNARE:
                color = Color.green;
                sprite.color = color;
                keyCode = KeyCode.J;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (CanBePressed)
            {
                gameObject.SetActive(false);
                lvl1.NoteHit();
                
            }
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.tag == "Activator")
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
