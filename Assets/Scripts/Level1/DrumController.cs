
using UnityEngine;

/// <summary>
/// Classe che controlla le azioni relative al trigger Drum del livello 1: a seconda che sia un Kick o uno Snare associa relativo file audio e colore da mettere quando viene colpito
/// </summary>
public class DrumController : MonoBehaviour
{
    public      SpriteRenderer sprite { get; private set; }
    
    public      Color baseColor;         //colore base
    private     Color triggeredColor;   //colore quando colpito
    public enum DRUM_TYPE //enumerazione che controlla il tipo di drum scelto
    {
        KICK_DRUM, SNARE_DRUM
    }
    [SerializeField] 
        private DRUM_TYPE drumType;

    [SerializeField] 
        private KeyCode KickTriggerKey, SnareTriggerKey;
    private KeyCode InputToCheck; //se è kick è uguale a KickTriggerKey, else SnareTriggerKey
   


    private void Awake()
    {
        //ottengo i componenti di base
        this.sprite = GetComponent<SpriteRenderer>();
        this.baseColor = sprite.color;

        //check del tipo di drum al momento della creazione dell'istanza
        switch (drumType)
        {
            case DRUM_TYPE.KICK_DRUM:
                this.triggeredColor = Color.blue;
                InputToCheck = KickTriggerKey;
                break;
            case DRUM_TYPE.SNARE_DRUM:
                this.triggeredColor = Color.green;
                InputToCheck = SnareTriggerKey;
                break;
        }

    }

    private void Update()
    {
        Drum_InputCheck(InputToCheck);
    }
    

    public void Drum_InputCheck(KeyCode keyCode)
    {
        if(Input.GetKeyDown(keyCode))
        {
            ChangeColor(triggeredColor);
        }
        else if(Input.GetKeyUp(keyCode))
        {
            ChangeColor();
        }
    }
    //senza argomenti cambia il colore in quello base "non suonato"
    public void ChangeColor()
    {
        this.sprite.color = baseColor;
    }
    public void ChangeColor(Color color)
    {
        this.sprite.color = color;
    }

    //metodi per la gestione dell'input
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //this.ChangeColor(triggeredColor);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //this.ChangeColor(baseColor);
    }

    private void OnMouseDown()
    {
        this.ChangeColor(triggeredColor);
    }
    private void OnMouseUp()
    {
        this.ChangeColor(baseColor);
    }


}
