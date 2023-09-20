using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private GiocoManager gameManager;

    [SerializeField] private int[] NumeroDiNote;
    [SerializeField] private Note nota;
    [SerializeField] private float beatTempo;
    private float delta_Tempo;
    private const float MAX_TEMPO = 400, MIN_TEMPO = 20;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GiocoManager.Instance;
        CheckAndCalculateTempo();
    }
    private void OnValidate()
    {
        CheckAndCalculateTempo();
    }
    private void CheckAndCalculateTempo()
    {
        if (beatTempo >= MIN_TEMPO && beatTempo <= MAX_TEMPO)
        {
            delta_Tempo = CalculateTempo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetHasStarted())
        {
            transform.position -= new Vector3(delta_Tempo * Time.deltaTime, 0f, 0f);
        }
        else
        {
            gameManager.SetHasStarted(true);
        }
    }

    public float CalculateTempo()
    {
        return beatTempo / 60f;
    }
}
