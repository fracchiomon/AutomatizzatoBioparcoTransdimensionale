using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    protected static ScoreManager Instance;
    private GameManager gameManager;
    [SerializeField] private SongManager songManager;

    public AudioSource hitSFX;
    //public AudioSource missSFX;
    [SerializeField] private TextMeshProUGUI ScoreText, ComboText;
    public  static int comboScore { get; private set; }
    private static uint      _score;

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
        ScoreText.text += " 0";
        ComboText.text = "Moltiplicatore: 0";
        comboScore = 0;
        _score = 0;
        
        

        Debug.Log($"Valore nota in ScoreManager: {_NoteValue}");
    }
    public void CalcolaValoreNota()
    {
        _NoteValue = (SongManager.GetNoteValueFromSong() > 0) ? SongManager.GetNoteValueFromSong() : 100; //in maniera simile a GuitarHero o altri rhythm game, il punteggio di una nota e' dato dal MAX SCORE diviso il numero di queste all'interno di una canzone
    }
    public static ScoreManager GetScoreManager() { return Instance; }
    public static void PerfectHit()
    {
        comboScore += 1;
        _score += (uint)(_NoteValue);
        Instance.hitSFX.Play();
    }
    public static void GoodHit()
    {
        //comboScore += 1; una good hit per ora non incrementa il combo meter
        _score += (uint)(_NoteValue * 0.75f);
        Instance.hitSFX.Play();
    }
    public static void Miss()
    {
        comboScore = 0;
        //Instance.missSFX.Play();
    }
    private void Update()
    {
        ScoreText.text = "Punteggio: " + _score.ToString();
        ComboText.text = "Moltiplicatore: " + comboScore.ToString();
    }
}