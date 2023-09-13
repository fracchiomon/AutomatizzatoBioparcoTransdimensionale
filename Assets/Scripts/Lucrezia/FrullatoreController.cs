using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Pool;

public class FrullatoreController : MonoBehaviour
{
    [SerializeField] private Transform outPoint;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector3 outForce;
    [SerializeField] private TextMeshProUGUI textRicetta;
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent nextLevel;
    private Action<String, int> BlenderMessage;
    private Action<float, float, string, string> UpdateBar;
    private float tot, rests, notes = 0;
    private SO_Recipe _ricetta;
    private Stack<Nota> contenuto = new Stack<Nota>();
    private Stack<GameObject> buttons = new Stack<GameObject>();
    private int messageT = 2;
    //list per l'obj pooling
    [SerializeField] private List<Nota> notesPool = new List<Nota>();

    public Nota GetNota(SO_NotaItem so_n)
    {
        Nota note = null;

        if(this.notesPool.Count > 0)
        {
            //se ho delle note nella pool, prendo quella che mi serve
            //il ciclo forech scorre "al contrario" così da poter fare il remove senza problemi
            foreach(Nota n in this.notesPool.Reverse<Nota>())
            {
                if (so_n.type.Equals(n.nome))
                {
                    //mi salvo la variabile nota e la rimuovo dalla pool
                    note = n;
                    this.notesPool.Remove(n);

                    //per spawnare correttamente la nota, le assegno la posizione/rotazione dello spawn point
                    note.gameObject.transform.position = this.spawnPoint.position;
                    note.gameObject.transform.rotation = this.spawnPoint.rotation;
                    //e azzero la risultante delle forze applicate al rigidbody
                    note.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    note.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

                    //attivo il gameObj
                    note.gameObject.SetActive(true);
                    break; //se ho trovato un istanza esco dal foreach
                }
            }
            if(note == null)
            {
                //se non ho trovato la nota che cercavo la istanzio
                note = Instantiate(so_n.prefab, this.spawnPoint.position, this.spawnPoint.rotation);
            }
        }
        else
        {
            //se non ci sono note la istanzio
            note = Instantiate(so_n.prefab, this.spawnPoint.position, this.spawnPoint.rotation);
        }
        return note;
    }

    IEnumerator ReturnNota(Nota n, float t)
    {
        this.notesPool.Add(n);
        yield return new WaitForSeconds(t);
        n.gameObject.SetActive(false);
    }

    public SO_Recipe ricetta
    {
        get
        {
            return this._ricetta;
        }
        set
        {
            this._ricetta = value;
        }
    }

    public void Start()
    {
        this.UpdateBar = FindObjectOfType<UI_CompletationPanel>().UpdateGraphics;
        UpdateRicetta(LevelManager.Instance.ricette[0]);
        this.BlenderMessage = FindObjectOfType<UI_Message>().SpawnMessage;
    }

    IEnumerator BlenderMixing()
    {
        this.animator.SetTrigger("Mixing");

        while (tot != 0)
        {
            OutMix();
        }

        this.BlenderMessage("Ricetta completata, sto mixando gli ingredienti!", messageT);

        yield return new WaitForSeconds(3);

        //screen fade
        ScreenFader.Instance.StartFadeToOpaque(
            (Action)(() =>
            {
                //bisogna caricare la prossima ricetta
                this.nextLevel.Invoke();
                this.UpdateBar((float)tot, this._ricetta.durataRecipe, this.notes.ToString(), this.rests.ToString());
                //per non avere riferimento alla completation bar posso usare un action!
                ScreenFader.Instance.StartFadeToTransparent(null);
            })
            );
    }

    public void UpdateRicetta(SO_Recipe ricetta)
    {
        this.ricetta = ricetta;
        this.textRicetta.text = this._ricetta.textRecipe;
    }

    public void Mix()
    {
        //se il totale, il n di pause e di note corrispondono a quelli della ricetta procedo al mix
        if(tot == this._ricetta.durataRecipe && rests == this._ricetta.restCounter && notes == this._ricetta.noteCounter)
        {
            StartCoroutine(BlenderMixing());
        }
        else
        {
            //Debug.Log("Mancano degli ingredienti!");
            this.BlenderMessage("Mancano degli ingredienti!", messageT);
        }
    }

    public void In(Nota toAdd, GameObject btn)
    {
        //Nota nota = toAdd.GetComponent<Nota>();
        contenuto.Push(toAdd);
        btn.SetActive(false);
        buttons.Push(btn);

        tot += toAdd.durata;

        if(toAdd.notaSO.isBreak)
        {
            this.rests++;
        }
        else
        {
            this.notes++;
        }

        this.UpdateBar(tot, this._ricetta.durataRecipe, this.notes.ToString(), this.rests.ToString());

        if (tot > this._ricetta.durataRecipe || rests > this._ricetta.restCounter || notes > this._ricetta.noteCounter)
        {
            Out();
            this.UpdateBar(tot, this._ricetta.durataRecipe, this.notes.ToString(), this.rests.ToString());
            this.BlenderMessage("Ingrediente sbagliato!", messageT);
        }
    }

    public void OutMix()
    {
        try
        {
            Nota nota = contenuto.Peek();

            buttons.Peek().SetActive(true);
            buttons.Pop();

            contenuto.Pop();

            tot -= nota.durata;
            if (nota.notaSO.isBreak)
            {
                this.rests--;
            }
            else
            {
                this.notes--;
            }

            this.UpdateBar(tot, this._ricetta.durataRecipe, this.notes.ToString(), this.rests.ToString());
            //Destroy(nota.gameObject, 3);
            StartCoroutine(ReturnNota(nota, 3));
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void Out()
    {
        
        try
        {
            Nota nota = contenuto.Peek();

            GameObject toPop = nota.gameObject;

            buttons.Peek().SetActive(true);
            buttons.Pop();
          
            contenuto.Pop();

            tot -= nota.durata;
            if (nota.notaSO.isBreak)
            {
                this.rests--;
            }
            else
            {
                this.notes--;
            }

            this.UpdateBar(tot, this._ricetta.durataRecipe, this.notes.ToString(), this.rests.ToString());
            toPop.gameObject.transform.position = this.outPoint.position; 
            toPop.GetComponent<Rigidbody>().AddForce(this.outForce);
            //Destroy(toPop, 2);
            StartCoroutine(ReturnNota(nota, 2));
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }

    }
}
