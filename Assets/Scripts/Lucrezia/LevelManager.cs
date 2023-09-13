using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    #region PARTE DEL SINGLETON
    private static LevelManager _instance;

    private LevelManager()
    {
        //fa creare oggetti solo all'interno della classe stessa
    }

    public static LevelManager Instance
    {
        get
        {
            //controllo se è registrato, perché poteri richiamare il get prima dell'awake
            if (_instance == null)
            {
                //lo cerco nella scena
                _instance = FindObjectOfType<LevelManager>(true);

                //controllo se l'ho trovato
                if (_instance == null)
                {
                    //non è stato trovato e quindi lo creo
                    //Resources.Load("Screen Fader Canvas"); //--> gli passo il nome di un file "" che si trova nella cartella Resource
                    GameObject go = Instantiate<GameObject>(Resources.Load("LevelManager") as GameObject);
                    _instance = go.GetComponent<LevelManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    //l'awake controlla che non ci sono altre istanze "di questo singleton" in scena
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

    [SerializeField] private SO_Recipe[] _ricette;
    private Action<SO_Recipe> updateFrullatore;
    private Action<SO_NotaItem[]> updateDispensa;
    private int score;
    private int recipeIndex;
    private float _time;

    public SO_Recipe[] ricette => this._ricette;
    public int time => (int) this._time;

    private void Start()
    {
        this.updateFrullatore = FindObjectOfType<FrullatoreController>().UpdateRicetta;
        this.updateDispensa = FindObjectOfType<UI_windowDispensa>().UpdateIngredienti;
        this.recipeIndex = 1;
    }

    private void Update()
    {
        //il punteggio verrà assegnato in base al tempo (fasce orarie) impiegato e agli sbagli commessi
        _time += Time.deltaTime;
    }

    public void RestartRicetta()
    {
        //così si potrebbe anche randomizzare lo spawn delle ricette
        updateFrullatore(this._ricette[recipeIndex]);
        updateDispensa(this._ricette[recipeIndex].ingredienti);
        this.recipeIndex++;
        if(this.recipeIndex == this._ricette.Length)
        {
            this.recipeIndex = 0;
        }
    }
}
