using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region parte del Singleton
    protected static SaveManager _instance;

    protected SaveManager()
    { }

    public static SaveManager Instance
    {
        get
        {
            //controllo se è registrato, perché poteri richiamare il get prima dell'awake
            if (_instance == null)
            {
                //lo cerco nella scena
                _instance = FindObjectOfType<SaveManager>(true);

                //controllo se l'ho trovato
                if (_instance == null)
                {
                    //non è stato trovato e quindi lo creo
                    //Resources.Load("Game"); //--> gli passo il nome di un file "" che si trova nella cartella Resource
                    GameObject go = Instantiate<GameObject>(Resources.Load("SaveManager") as GameObject);
                    _instance = go.GetComponent<SaveManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    private int _bestRythmicon;
    private int _bestNoteHunt;
    private int _bestCookingNotes;
    private int _bestFindTheNote;
    private int _bestWhackANote;

    //per ogni gioco, quando si vince si richiama il bestNomeLivello per settare il punteggio
    //e si richiama il Save della classe statica SaveLoad_JSON

    //quando si inizia dal menù principale, se non c'è un file di salvataggio si settano a 0 tutti punteggi

    public int bestRythmicon
    {
        get
        {
            return this._bestRythmicon;
        }
        set
        {
            if(value > this._bestRythmicon)
            {
                this._bestRythmicon = value;
            }
        }

    }

    public int bestNoteHunt
    {
        get
        {
            return this._bestNoteHunt;
        }
        set
        {
            if (value > this._bestNoteHunt)
            {
                this._bestNoteHunt = value;
            }
        }

    }

    public int bestCookingNotes
    {
        get
        {
            return this._bestCookingNotes;
        }
        set
        {
            if (value > this._bestCookingNotes)
            {
                this._bestCookingNotes = value;
            }
        }

    }

    public int bestFindTheNote
    {
        get
        {
            return this._bestFindTheNote;
        }
        set
        {
            if (value > this._bestFindTheNote)
            {
                this._bestFindTheNote = value;
            }
        }

    }

    public int bestWhackANote
    {
        get
        {
            return this._bestWhackANote;
        }
        set
        {
            if (value > this._bestWhackANote)
            {
                this._bestWhackANote = value;
            }
        }

    }

    public void Save()
    {
        SaveLoad_JSON.SaveToFile(SaveLoad_JSON.GenerateSaveData());
    }

    public void Load()
    {
        if (SaveLoad_JSON.DoesSaveFileExist())
        {
            SaveLoad_JSON.ApplySaveData(SaveLoad_JSON.LoadFromFile());
        }
        //se non trova un file, setta i punteggi a zero
        else
        {
            this._bestRythmicon = 0;
            this._bestNoteHunt = 0;
            this._bestCookingNotes = 0;
            this._bestFindTheNote = 0;
            this._bestWhackANote = 0;
        }
    }

}
