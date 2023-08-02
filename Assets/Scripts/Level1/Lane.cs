using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private Color noteColor;

    [SerializeField] Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction; //Gestisce la nota al quale e' assegnata la lane
    public KeyCode input; //tasto assegnato
    [SerializeField] private GameObject notePrefab;
    private List<Note> notes = new List<Note>();
    private List<double> timeStamps = new List<double>(); //lista di timestamps corrispondenti alle note

    private int spawnIndex = 0; //indice della nota spawnata
    private int inputIndex = 0; //indice della nota colpita

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array) //Prende l'array di note e lo riempie con le note corrispondenti al suo canale (la sua "nota")
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap()); //conversione della metrica di tempo dal formato MIDI a quello "classico"
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f); //aggiunge il valore ottenuto alla lista
            }
        }
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
                note.GetComponent<Note>().SetColor(noteColor);
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                note.name = "Note(Clone) #" + spawnIndex.ToString();
                spawnIndex++; //incrementa l'indice
                Debug.Log($"Spawn Y: {SongManager.Instance.noteSpawnY}; Spawn Y attuale: {note.transform.position.y}");
            }
        }

        if (inputIndex < timeStamps.Count) //se non sono state colpite tutte le note?
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(); //Nota colpita
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else //Nota non colpita perfettamente
                {
                    SpegniNota();
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            if (timeStamp + marginOfError <= audioTime) //nota non colpita
            {
                Miss();
                print($"Missed {inputIndex} note");
                Invoke(nameof(SpegniNota), 3f);
                inputIndex++;
            }
        }

    }
    private void SpegniNota()
    {
        notes[inputIndex].gameObject.SetActive(false);
    }
    private void Hit()
    {
        ScoreManager.PerfectHit(); //suona l'efx e incrementa punteggio e indicatore combo
    }

    
    private void Miss()
    {
        ScoreManager.Miss(); //suona l'efx e resetta combo
    }
}
