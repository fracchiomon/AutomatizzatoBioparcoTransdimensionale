using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Debug
using System.Collections;

public class Lane : MonoBehaviour
{
    public bool IsDebugEnabled;

    public TextMeshProUGUI DEBUG_TEXT;

    [SerializeField] private Color noteColor;

    //Gestisce la nota al quale e' assegnata la lane
    [SerializeField] Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;

    //tasto assegnato
    public KeyCode input;

    //prefab della nota
    [SerializeField] private GameObject notePrefab;
    private List<Note> notes = new List<Note>();

    //private List<double> timeStamps = new List<double>();

    //lista di timestamps corrispondenti alle note
    private List<float> timeStamps = new List<float>();
    private int spawnIndex = 0; //indice della nota spawnata
    private int inputIndex = 0; //indice della nota colpita

    public int maxMissedNotes { get; private set; }
    public int missedNotes { get; private set; }


    private void Start()
    {
        IsDebugEnabled = SongManager.IsDebugEnabled;
        //------------DEBUG_SECTION-------------//
        if (IsDebugEnabled)
        {
            DEBUG_TEXT.gameObject.SetActive(true);

            DEBUG_TEXT.SetText("START");
        }
        //------------END_DEBUG_SECTION--------//
        maxMissedNotes = ScoreManager.Instance.GetMaxMissedNotes();
        missedNotes = ScoreManager.Instance.GetMissedNotes();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnNote());
        if (inputIndex < timeStamps.Count) //se non sono state colpite tutte le note?
        {
            float timeStamp = timeStamps[inputIndex];
            float marginOfError = SongManager.Instance.marginOfError;
            float audioTime = (SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000f));

            StartCoroutine(GestioneInput(timeStamp, marginOfError, audioTime));
        }

    }

    private System.Collections.IEnumerator GestioneInput(float timeStamp, float marginOfError, float audioTime)
    {

        if (Input.GetKeyDown(input))
        {
            //------------DEBUG_SECTION-------------//
            if (IsDebugEnabled)
            {
                DEBUG_TEXT.SetText("AudioSourceTime = " + SongManager.GetAudioSourceTime().ToString());
                DEBUG_TEXT.SetText(DEBUG_TEXT.text + $"\nMathf.Abs((float)(audioTime - timeStamp)) = {Mathf.Abs((float)(audioTime - timeStamp))}");
            }
            //------------END_DEBUG_SECTION--------//

            if (notes[inputIndex].CanBePressed)
            {

                if (Mathf.Abs((float)(audioTime - timeStamp)) < (float)marginOfError)
                {
                    //Debug.Log("AudioSourceTime = " + SongManager.GetAudioSourceTime().ToString());

                    Hit(); //Nota colpita

                    if (IsDebugEnabled)
                        print($"Hit on {inputIndex} note");


                    Destroy(notes[inputIndex].gameObject, 0.5f);
                    inputIndex++;

                    if (IsDebugEnabled)
                        print($"nuovo inputIndex: {inputIndex}");
                    yield break;
                }
                else //Nota non colpita perfettamente
                {
                    //SpegniNota();
                    NormalHit();
                    Destroy(notes[inputIndex].gameObject, 1f);
                    inputIndex++;

                    if (IsDebugEnabled)
                        print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    yield break;
                }
            }
        }
        if (timeStamp + marginOfError <= audioTime) //nota non colpita
        {

            if (IsDebugEnabled)
            {
                DEBUG_TEXT.SetText($"Miss. timeStamp + marginOfError = {timeStamp + marginOfError} - audioTime = {audioTime}");
                print($"Missed {inputIndex} note");

            }

            Miss();


            //Invoke(nameof(SpegniNota), 3f);
            inputIndex++;
            yield break;
        }
        yield return null;
    }



    private System.Collections.IEnumerator SpawnNote()
    {
        if (spawnIndex < timeStamps.Count) //gestione dello spawn finche' ci sono note nella lista
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime) //se il tempo ottenuto dalla canzone in corso e' maggioreuguale della differenza tra il timestamp della nota precedene e quella da spawnare(?) spawna la nuova nota
            {
                GameObject note = CreaNuovaNotaPrefab();
                spawnIndex++; //incrementa l'indice

                if (IsDebugEnabled)
                    Debug.Log("nota: " + note.name + "spawnIndex = " + spawnIndex);
                //Debug.Log($"Spawn Y: {SongManager.Instance.noteSpawnY}; Spawn Y attuale: {note.transform.position.y}");
                yield break;
            }
        }
        yield return null;
    }

    private GameObject CreaNuovaNotaPrefab()
    {
        //Prende la transform dello spawn
        Transform NoteSpawnTransform = transform;
        //istanzia il prefab e assegna la transform
        var note = Instantiate(notePrefab, NoteSpawnTransform);
        note.transform.position.Set(note.transform.position.x, note.transform.position.y + SongManager.Instance.noteSpawnY, 0f);
        //aggiunge la nota alla lista
        notes.Add(note.GetComponent<Note>());

        //testo di debug su nota
        if (IsDebugEnabled)
            note.GetComponent<Note>().DEBUG_TEXT.rectTransform.position = note.transform.position;

        //imposta il colore della nota
        note.GetComponent<Note>().SetColor(noteColor);
        //assegna alla nota creata il timestamp
        note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
        //infine assegna un nome alla nota
        note.name = "Note(Clone) #" + spawnIndex.ToString();
        return note;
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array) //Prende l'array di note e lo riempie con le note corrispondenti al suo canale (la sua "nota")
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                //conversione della metrica di tempo dal formato MIDI a quello "classico"
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());

                //timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f); //aggiunge il valore ottenuto alla lista
                timeStamps.Add(metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + metricTimeSpan.Milliseconds / 1000f);

                //------------DEBUG_SECTION-------------//
                if (IsDebugEnabled)
                {
                    //Debug.Log((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                    //Debug.Log("controllo MetricTimeSpan.Minutes " + metricTimeSpan.Minutes);
                    //Debug.Log("controllo MetricTimeSpan.Minutes * 60f -  " + metricTimeSpan.Minutes * 60f);
                    //Debug.Log("controllo MetricTimeSpan.Seconds " + metricTimeSpan.Seconds);
                    //Debug.Log("controllo MetricTimeSpan.Milli " + metricTimeSpan.Milliseconds);
                    //Debug.Log("controllo MetricTimeSpan.Micro " + metricTimeSpan.Milliseconds / 1000f);

                }
                //------------END_DEBUG_SECTION--------//


            }
        }
        //------------DEBUG_SECTION-------------//
        if (IsDebugEnabled)
        {
            Debug.Log($"numero di timestamps = {timeStamps.Count}");
            Debug.Log("Lane: " + this.name + "\ttimestamps: ");
            DEBUG_STAMPA_LISTA(timeStamps);
        }
        //------------END_DEBUG_SECTION--------//


    }


    //------------DEBUG_SECTION-------------//
    public void DEBUG_STAMPA_LISTA(List<float> ts)
    {
        foreach (float t in ts)
        {
            print($"timestamp n.{ts.IndexOf(t)} = {t}");
        }
    }
    //------------END_DEBUG_SECTION--------//



    private void SpegniNota()
    {
        notes[inputIndex].gameObject.SetActive(false);
    }
    private void Hit()
    {
        notes[inputIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
        notes[inputIndex].GetComponent<Note>().CanBePressed = false;


        ScoreManager.PerfectHit(); //suona l'efx e incrementa punteggio e indicatore combo
    }

    private void NormalHit()
    {
        notes[inputIndex].GetComponent<SpriteRenderer>().color = Color.black;
        notes[inputIndex].GetComponent<Note>().CanBePressed = false;
        ScoreManager.NormalHit();
    }

    private void Miss()
    {
        notes[inputIndex].GetComponent<SpriteRenderer>().color = Color.white;
        notes[inputIndex].GetComponent<Note>().CanBePressed = false;
        missedNotes++;
        ScoreManager.Instance.SetMissedNotes(missedNotes);
        ScoreManager.Miss(); //suona l'efx e resetta combo
    }


}


//------------DEBUG_SECTION-------------//
//------------END_DEBUG_SECTION--------//