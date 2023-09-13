using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Song : MonoBehaviour
{
    public AudioSource songSource { get; private set; }
    public AudioClip clip { get; private set; }
    public string SONG_NAME, SONG_ARTIST;
    public uint SONG_TEMPO;
    public enum SONG_DIFFICULTY
    {
        EASY, MEDIUM, HARD
    }
    public SONG_DIFFICULTY difficulty;
    public string MIDI_SONG_PATH { get; private set; } //ricostruisce il path a partire da MIDI_SONG_NAME, cioè semplicemente il nome e la difficoltà
    public string MIDI_SONG_NAME;
    public string MIDI_SONG_LEVEL;
    private string MIDI_SONG_DIFFICULTY; //serve per costruzione del path

    // Start is called before the first frame update
    void Start()
    {
        songSource = GetComponent<AudioSource>();
        clip = songSource.clip;
        switch (difficulty)
        {
            case SONG_DIFFICULTY.EASY:
                MIDI_SONG_DIFFICULTY = "Easy";
                break;
            case SONG_DIFFICULTY.MEDIUM:
                MIDI_SONG_DIFFICULTY = "Medium";
                break;
            case SONG_DIFFICULTY.HARD:
                MIDI_SONG_DIFFICULTY = "Hard";
                break;
            default:
                MIDI_SONG_DIFFICULTY = "Easy";
                break;
        }
        TentaLetturaFile();

    }
    private void TentaLetturaFile()
    {
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite()); //nel caso sia build Web leggeremo da un indirizzo http(s), altrimenti da un file

        }
        else
        {
            ReadFromFile();
        }
    }

    /// <summary>
    /// Coroutine per lettura asincrona del MIDI file da un indirizzo http, memorizzato in uno stream di dati mandato alla funzione GetDataFromMidi()
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private IEnumerator ReadFromWebsite()
    {
        MIDI_SONG_PATH = Application.streamingAssetsPath + "/" + "MIDI/" + MIDI_SONG_LEVEL + "/" + MIDI_SONG_DIFFICULTY + "/" + MIDI_SONG_NAME;

        using UnityWebRequest www = UnityWebRequest.Get(MIDI_SONG_PATH);


        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) //catch di errori di connessione o protocollo
        {
            Debug.LogError(www.error);
            throw new Exception("Errore di connessione");
        }
        print(MIDI_SONG_PATH);

    }

    /// <summary>
    /// Legge un file MIDI a partire dalla location /Assets/StreamingAssets
    /// </summary>
    private void ReadFromFile()
    {
        MIDI_SONG_PATH = Application.streamingAssetsPath + "/" + "MIDI/" + MIDI_SONG_LEVEL + "/" + MIDI_SONG_DIFFICULTY + "/" + MIDI_SONG_NAME;
        print(MIDI_SONG_PATH);

    }



}
