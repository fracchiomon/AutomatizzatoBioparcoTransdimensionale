using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public bool IsDebugEnabled;

    protected static ScoreManager Instance;
    [SerializeField] private SongManager songManager;

    public AudioSource hitSFX;
    //public AudioSource missSFX;
    [SerializeField] private TextMeshProUGUI ScoreText, ComboText;
    public static int ComboScore { get; private set; }
    private static uint _score;

    private static float _NoteValue;
    public static readonly uint _MAX_SCORE = 1000000;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            Instance = this;
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


        if (IsDebugEnabled)
            Debug.Log($"Valore nota in ScoreManager: {_NoteValue}");
    }
    void Update()
    {
        ScoreText.text = "Punteggio: " + _score.ToString();
        ComboText.text = "Moltiplicatore: " + ComboScore.ToString();
    }
    public static IEnumerator CalcolaValoreNota()
    {
        _NoteValue = (SongManager.GetNoteScoreValueFromSong() > 0) ? SongManager.GetNoteScoreValueFromSong() : 100; //in maniera simile a GuitarHero o altri rhythm game, il punteggio di una nota e' dato dal MAX SCORE diviso il numero di queste all'interno di una canzone
        yield return _NoteValue;
    }
    public static ScoreManager GetScoreManager() { return Instance; }
    public static void PerfectHit()
    {
        ComboScore += 1;
        _score += (uint)(_NoteValue);
        Instance.hitSFX.Play();
    }

    public static void Miss()
    {
        ComboScore = 0;
        //Instance.missSFX.Play();
    }

}