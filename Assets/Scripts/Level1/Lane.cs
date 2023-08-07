using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Debug

public class Lane : MonoBehaviour
{
    public bool IsDebugEnabled;

    public TextMeshProUGUI DEBUG_TEXT;

    [SerializeField] private Color noteColor;

    [SerializeField] Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction; //Gestisce la nota al quale e' assegnata la lane
    public KeyCode input; //tasto assegnato
    [SerializeField] private GameObject notePrefab;
    private List<Note> notes = new List<Note>();
    //private List<double> timeStamps = new List<double>(); //lista di timestamps corrispondenti alle note
    private List<float> timeStamps = new List<float>();
    private int spawnIndex = 0; //indice della nota spawnata
    private int inputIndex = 0; //indice della nota colpita


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

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count) //gestione dello spawn finche' ci sono note nella lista
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime) //se il tempo ottenuto dalla canzone in corso e' maggioreuguale della differenza tra il timestamp della nota precedene e quella da spawnare(?) spawna la nuova nota
            {
                Transform NoteSpawnTransform = transform;
                var note = Instantiate(notePrefab, NoteSpawnTransform);
                note.transform.position.Set(note.transform.position.x, note.transform.position.y + SongManager.Instance.noteSpawnY, 0f);
                notes.Add(note.GetComponent<Note>());

                //testo di debug su nota
                if (IsDebugEnabled)
                    note.GetComponent<Note>().DEBUG_TEXT.rectTransform.position = note.transform.position;

                note.GetComponent<Note>().SetColor(noteColor);
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                note.name = "Note(Clone) #" + spawnIndex.ToString();
                spawnIndex++; //incrementa l'indice

                if (IsDebugEnabled)
                    Debug.Log("nota: " + note.name + "spawnIndex = " + spawnIndex);
                //Debug.Log($"Spawn Y: {SongManager.Instance.noteSpawnY}; Spawn Y attuale: {note.transform.position.y}");
            }
        }

        if (inputIndex < timeStamps.Count) //se non sono state colpite tutte le note?
        {
            float timeStamp = timeStamps[inputIndex];
            float marginOfError = SongManager.Instance.marginOfError;
            float audioTime = (float)(SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0));

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
                    }
                    else //Nota non colpita perfettamente
                    {
                        //SpegniNota();
                        notes[inputIndex].GetComponent<SpriteRenderer>().color = Color.black;
                        notes[inputIndex].GetComponent<Note>().CanBePressed = false;

                        if (IsDebugEnabled)
                            print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
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
            }
        }

    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array) //Prende l'array di note e lo riempie con le note corrispondenti al suo canale (la sua "nota")
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap()); //conversione della metrica di tempo dal formato MIDI a quello "classico"

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

        ScoreManager.PerfectHit(); //suona l'efx e incrementa punteggio e indicatore combo
    }


    private void Miss()
    {
        notes[inputIndex].GetComponent<SpriteRenderer>().color = Color.white;
        notes[inputIndex].GetComponent<Note>().CanBePressed = false;

        ScoreManager.Miss(); //suona l'efx e resetta combo
    }
}


//------------DEBUG_SECTION-------------//
//------------END_DEBUG_SECTION--------//