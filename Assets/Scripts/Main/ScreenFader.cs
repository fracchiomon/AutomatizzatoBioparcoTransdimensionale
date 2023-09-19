using System.Collections;
using UnityEngine;
using System;
using FMOD;
using FMODUnity;

public class ScreenFader : MonoBehaviour
{
    #region PARTE DEL SINGLETON
    private static ScreenFader _instance;

    private ScreenFader()
    {
        //fa creare oggetti solo all'interno della classe stessa
    }

    public static ScreenFader Instance
    {
        get
        {
            //controllo se è registrato, perché poteri richiamare il get prima dell'awake
            if (_instance == null)
            {
                //lo cerco nella scena
                _instance = FindObjectOfType<ScreenFader>(true);

                //controllo se l'ho trovato
                if (_instance == null)
                {
                    //non è stato trovato e quindi lo creo
                    //Resources.Load("Screen Fader Canvas"); //--> gli passo il nome di un file "" che si trova nella cartella Resource
                    GameObject go = Instantiate<GameObject>(Resources.Load("Screen Fader Canvas") as GameObject);
                    _instance = go.GetComponent<ScreenFader>();
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

    public enum STATE
    {
        TRANSPARENT,
        TO_OPAQUE,
        OPAQUE,
        TO_TRANSPARENT
    }

    [SerializeField] private STATE state = STATE.OPAQUE;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 2.5f;

    private BackGroundMusic audioSourceBgMusic;

    private Action onFadeCompleted;
    private float timer = 0;

    private IEnumerator couroutine;

    public void StartFadeToOpaque(Action callback)
    {
        if (this.audioSourceBgMusic == null)
        {
            this.audioSourceBgMusic = FindObjectOfType<BackGroundMusic>();
        }

        this.gameObject.SetActive(true);
        this.onFadeCompleted = callback;

        //se ho già una couroutine attiva, la interrompo prima di far ripartire quella
        //verso l'opacità 
        if (this.couroutine != null)
        {
            StopCoroutine(this.couroutine);
        }

        this.couroutine = CouroutiuneToOpaque();
        StartCoroutine(this.couroutine);
    }

    public void StartFadeToTransparent(Action callback)
    {
        if (this.audioSourceBgMusic == null)
        {
            this.audioSourceBgMusic = FindObjectOfType<BackGroundMusic>();
        }

        this.gameObject.SetActive(true);

        this.onFadeCompleted = callback;

        //se ho già una couroutine attiva, la interrompo prima di far ripartire quella
        //verso la trasparenza 
        if (this.couroutine != null)
        {
            StopCoroutine(this.couroutine);
        }

        this.couroutine = CouroutineToTransparent();
        StartCoroutine(this.couroutine);
    }

    private IEnumerator CouroutiuneToOpaque()
    {
        //inizio
        this.timer = 0;
        this.state = STATE.TO_OPAQUE;

        //svolgimento
        yield return null; //aspetto il frame successivo
        while (this.timer < this.fadeDuration)
        {
            this.timer += Time.deltaTime;
            this.canvasGroup.alpha = Mathf.Lerp(0, 1, this.timer / this.fadeDuration); //media pesata fra min e max

            if (this.audioSourceBgMusic != null)
            {
                this.audioSourceBgMusic.audioSource.EventInstance.setVolume(Mathf.Lerp(0, 1, 1 - this.timer / this.fadeDuration));
            }

            yield return null;
        }

        //conclusione
        this.timer = 0;
        this.canvasGroup.alpha = 1;

        if (this.audioSourceBgMusic != null)
        {
            this.audioSourceBgMusic.audioSource.EventInstance.setVolume(0);
        }

        this.state = STATE.OPAQUE;
        if (this.onFadeCompleted != null)
        {
            this.onFadeCompleted();
        }

    }

    private IEnumerator CouroutineToTransparent()
    {
        //inizio
        this.timer = 0;
        this.state = STATE.TO_TRANSPARENT;

        //svolgimento
        yield return null; //aspetto il frame successivo
        while (this.timer < this.fadeDuration)
        {
            this.timer += Time.deltaTime;
            this.canvasGroup.alpha = Mathf.Lerp(0, 1, 1 - this.timer / this.fadeDuration); //media pesata fra min e max

            if (this.audioSourceBgMusic != null)
            {
                this.audioSourceBgMusic.audioSource.EventInstance.setVolume(Mathf.Lerp(0, 1, this.timer / this.fadeDuration));
            }

            yield return null;
        }

        //conclusione
        this.timer = 0;
        this.canvasGroup.alpha = 0;

        if (this.audioSourceBgMusic != null)
        {
            this.audioSourceBgMusic.audioSource.EventInstance.setVolume(1f);
        }

        this.state = STATE.TRANSPARENT;
        if (this.onFadeCompleted != null)
        {
            this.onFadeCompleted();
        }

    }

    private void Start()
    {
        this.audioSourceBgMusic = FindObjectOfType<BackGroundMusic>();
        if (this.canvasGroup == null)
        {
            this.canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
