using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    #region PARTE DEL SINGLETON
    private static DontDestroyOnLoad _instance;

    public static DontDestroyOnLoad Instance
    {
        get
        {
            //controllo se è registrato, perché poteri richiamare il get prima dell'awake
            if (_instance == null)
            {
                //lo cerco nella scena
                _instance = FindObjectOfType<DontDestroyOnLoad>(true);

                //controllo se l'ho trovato
                if (_instance == null)
                {
                    //non è stato trovato e quindi lo creo
                    //Resources.Load("Screen Fader Canvas"); //--> gli passo il nome di un file "" che si trova nella cartella Resource
                    GameObject go = Instantiate<GameObject>(Resources.Load("DontDestroyOnLoad") as GameObject);
                    _instance = go.GetComponent<DontDestroyOnLoad>();
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
}
