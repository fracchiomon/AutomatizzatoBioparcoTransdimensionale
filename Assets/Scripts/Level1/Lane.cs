using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    
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
                var note = Instantiate(notePrefab, transform); 
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++; //incrementa l'indice
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
                    PerfectHit(); //Nota colpita
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else //Nota non colpita perfettamente
                {
                    GoodHit(); //per ora è colpita ugualmente
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            if (timeStamp + marginOfError <= audioTime) //nota non colpita
            {
                Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }

    }
    private void PerfectHit()
    {
        ScoreManager.PerfectHit(); //suona l'efx e incrementa punteggio e indicatore combo
    }

    private void GoodHit()
    {
        ScoreManager.GoodHit(); //suona l'efx e incrementa punteggio e indicatore combo
    }
    private void Miss()
    {
        ScoreManager.Miss(); //suona l'efx e resetta combo
    }
}
