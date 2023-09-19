using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public bool IsDebugEnabled;

    public static ScoreManager Instance;
    [SerializeField] private SongManager songManager;

    public AudioSource hitSFX;
    //public AudioSource missSFX;
    [SerializeField] private TextMeshProUGUI ScoreText, ComboText;
    public static int ComboScore { get; private set; }
    private static int _scoreMultiplier;
    public static int GetScoreMultiplier() { return _scoreMultiplier; }
    public static void SetScoreMultiplier(int scoreMult) { _scoreMultiplier = scoreMult; }
    private static uint _score;

    private static float _NoteValue;
    public static readonly uint _MAX_SCORE = 100000;

    private Canvas _quitGameCanvas;
    private Canvas _songSelectCanvas;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            Instance = this;
        }
        try
        {
            _quitGameCanvas = FindObjectOfType<QuitGameCanvas>(includeInactive: true).GetComponentInParent<Canvas>();
            _songSelectCanvas = FindObjectOfType<SongSelectCanvas>(includeInactive: true).GetComponentInParent<Canvas>();
            if (_quitGameCanvas != null)
                _quitGameCanvas.enabled = false;
            if (_songSelectCanvas != null)
                _songSelectCanvas.enabled = true;
        }
        catch
        {
            if (IsDebugEnabled)
            {
                print(new Exception().Message);
            }
        }
    }
    void Start()
    {
        songManager = SongManager.Instance;
        IsDebugEnabled = songManager.nonStaticIsDebugEnabled;
        ScoreText.text += " 0";
        ComboText.text = "Moltiplicatore: 0";
        ComboScore = 0;
        _score = 0;
        _scoreMultiplier = 0;


        if (IsDebugEnabled)
            Debug.Log($"Valore nota in ScoreManager: {_NoteValue}");
    }
    void Update()
    {
        StartCoroutine(CheckAndUpdateScoreMultiplier());
        ScoreText.text = "Punteggio: " + _score.ToString();
        ComboText.text = "Moltiplicatore: " + GetScoreMultiplier().ToString();
        ControllaInput();
        SongManager.CheckEndGame();
        if (IsDebugEnabled)
        {
            if (SongManager.Instance.audioSource != null)
            {
                print($"Tempo passato: {SongManager.GetAudioSourceTime()}\nTempo canzone: {SongManager.Instance.audioSource.clip.length}");
            }
        }

    }


    /// <summary>
    /// Sfrutto l'Update di ScoreManager per controllare se Utente digita alcuni comandi chiave come ESC per tornare al menu
    /// </summary>
    /// <returns></returns>
    public void ControllaInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GiocoManager.Instance.RHYTHMICON_ConfermaTornaAlMenu();
            //if (_songSelectCanvas.enabled) _songSelectCanvas.enabled = false;
            //_quitGameCanvas.enabled = true;
            //SongManager.Instance.PauseSong();


        }

    }
    /// <summary>
    /// in maniera simile a GuitarHero o altri rhythm game, il punteggio di una nota e' dato dal MAX SCORE diviso il numero di queste all'interno di una canzone
    /// </summary>
    /// <returns></returns>
    public IEnumerator CalcolaValoreNota()
    {
        _NoteValue = (SongManager.GetNoteScoreValueFromSong() > 0) ? SongManager.GetNoteScoreValueFromSong() / 10f : 100;
        yield return _NoteValue;
    }
    public static ScoreManager GetScoreManager() { return Instance; }
    public static void PerfectHit()
    {
        ComboScore += 1;
        _score += (uint)(_NoteValue * GetScoreMultiplier());
        Instance.hitSFX.pitch = UnityEngine.Random.Range(0.985f, 1.085f);
        Instance.hitSFX.Play();
    }

    public static IEnumerator CheckAndUpdateScoreMultiplier()
    {
        int multPer1_Threshold = 0, multPer2_Threshold = 4, multPer3_Threshold = multPer2_Threshold * 2, multPer4_Threshold = multPer2_Threshold * 3, multPer5_Threshold = multPer2_Threshold * 5;  //threshold grezze per l'incremento del moltiplicatore del punteggio

        if (multPer1_Threshold < ComboScore && ComboScore < multPer2_Threshold)
        {
            SetScoreMultiplier(1);

        }
        else if (multPer2_Threshold < ComboScore && ComboScore < multPer3_Threshold)
        {
            SetScoreMultiplier(2);

        }
        else if (multPer3_Threshold < ComboScore && ComboScore < multPer4_Threshold)
        {
            SetScoreMultiplier(3);

        }
        else if (multPer4_Threshold < ComboScore && ComboScore < multPer5_Threshold)
        {
            SetScoreMultiplier(4);
        }
        else if (ComboScore >= multPer5_Threshold)
            SetScoreMultiplier(5);
        yield return null;

    }

    public static void Miss()
    {
        ComboScore = 0;
        SetScoreMultiplier(0);
        //Instance.missSFX.Play();
    }

}