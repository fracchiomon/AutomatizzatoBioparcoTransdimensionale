using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public bool IsDebugEnabled;

    public static ScoreManager Instance;
    [SerializeField] private SongManager songManager;


    [SerializeField] private AudioSource hitSFX, normalHitSFX, missSFX;
    [SerializeField] private TextMeshProUGUI ScoreText, ComboText;

    private int maxMissedNotes, missedNotes;
    public int GetMaxMissedNotes() { return maxMissedNotes; }
    public void SetMaxMissedNotes(int numOfMaxMissNotes) { this.missedNotes = numOfMaxMissNotes; }
    public int GetMissedNotes() { return missedNotes; }
    public void SetMissedNotes(int numOfMissNotes) { this.missedNotes = numOfMissNotes; }


    public static int ComboScore { get; private set; }
    private static int _scoreMultiplier;
    public static int GetScoreMultiplier() { return _scoreMultiplier; }
    public static void SetScoreMultiplier(int scoreMult) { _scoreMultiplier = scoreMult; }
    private static int _score;

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
        SetMissedNotes(0);


        if (IsDebugEnabled)
            Debug.Log($"Valore nota in ScoreManager: {_NoteValue}");
    }

    /// <summary>
    /// Uso l'update per funzioni chiave come il controllo del fine gioco e l'aggiornamento del punteggio
    /// </summary>
    void Update()
    {
        CheckAndUpdateScoreMultiplier();
        ScoreText.text = "Punteggio: " + _score.ToString();
        ComboText.text = "Moltiplicatore: " + GetScoreMultiplier().ToString();
        ControllaInput();
        if (CheckEndGame())
            End();

        if (IsDebugEnabled)
        {
            if (SongManager.Instance.audioSource != null)
            {
                print($"Tempo passato: {SongManager.GetAudioSourceTime()}\nTempo canzone: {SongManager.Instance.audioSource.clip.length}");
                print($"EndGame? => {CheckEndGame()}\nScena: {SceneManager.GetActiveScene().name}, caricata: {SceneManager.GetActiveScene().isLoaded}");
            }
        }


    }
    public bool CheckEndGame()
    {
        if (SongManager.Instance.audioSource != null)
        {
            if (SongManager.Instance.audioSource.clip.length <= SongManager.GetAudioSourceTime())
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Se il gioco finisce, interrompe la canzone, salva i punteggi e richiama la scena di Vittoria
    /// </summary>
    void End()
    {
        SongManager.Instance.StopSong();

        ScoreForMiniGame.Instance.SetHighScore(_score);
        SaveManager.Instance.bestRythmicon = _score;
        if (IsDebugEnabled)
        {
            print(SaveManager.Instance.bestRythmicon);
        }
        SaveManager.Instance.Save();
        SceneManager.LoadScene(sceneName: "Victory");
    }
    /// <summary>
    /// Sfrutto l'Update di ScoreManager per controllare se Utente digita alcuni comandi chiave come ESC per tornare al menu
    /// </summary>
    /// <returns></returns>
    public void ControllaInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GiocoManager.Instance.ToMainMenu();
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
        _score += (int)(_NoteValue * GetScoreMultiplier());
        Instance.hitSFX.pitch = UnityEngine.Random.Range(0.985f, 1.085f);
        Instance.hitSFX.Play();
    }
    public static void NormalHit()
    {
        _score += (int)(_NoteValue);
        Instance.normalHitSFX.pitch = UnityEngine.Random.Range(0.985f, 1.085f);
        Instance.normalHitSFX.Play();
    }

    [SerializeField] private int[] MultiplierThresholds;  //threshold grezze per l'incremento del moltiplicatore del punteggio
    int multPer1_Threshold = 0;

    public static void CheckAndUpdateScoreMultiplier()
    {

        if (multPer1_Threshold < ComboScore && ComboScore < MultiplierThresholds[0])
        {
            SetScoreMultiplier(1);

        }
        else if (MultiplierThresholds[1] < ComboScore && ComboScore < MultiplierThresholds[2])
        {
            SetScoreMultiplier(2);

        }
        else if (MultiplierThresholds[2] < ComboScore && ComboScore < MultiplierThresholds[3])
        {
            SetScoreMultiplier(3);

        }
        else if (MultiplierThresholds[3] < ComboScore && ComboScore < MultiplierThresholds[4])
        {
            SetScoreMultiplier(4);
        }
        else if (ComboScore >= MultiplierThresholds[4])
            SetScoreMultiplier(5);
    }

    public static void Miss()
    {
        ComboScore = 0;
        SetScoreMultiplier(0);
        Instance.missSFX.Play();
    }

}