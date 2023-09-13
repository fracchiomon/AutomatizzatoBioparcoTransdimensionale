using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1_Control : MonoBehaviour
{
    private GiocoManager gameManager;

    protected static Level1_Control Instance;

    [SerializeField] private TextMeshProUGUI ScoreText;
    private const uint _NoteValue = 100;
    private const uint _MAX_SCORE = 1000000;
    private uint       _score;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text += " 0";
        _score = 0;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static Level1_Control GetInstance() { return Instance; }

    public void NoteHit()
    {
        _score += _NoteValue;
        ScoreText.text = "Punteggio: " + _score.ToString();
    }
}
