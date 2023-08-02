using System.Collections;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance { get; private set; } //per richiamare istanza di questo oggetto
    public ScoreManager scoreManager;
    public AudioSource audioSource; //contiene la canzone
    public Lane[] lanes; //gestiscono le "corsie" sulle quali viaggeranno le note
    public float songDelayInSeconds; //tempo tra una nota e l'altra
    public double marginOfError; // in seconds

    public int inputDelayInMilliseconds; //per controllare eventuale lag dell'input dell'utente (calibrazione)

    //TODO: implementare piu songs in classe Song
    public string fileLocation; //dove si trova la canzone
    public int BPM;
    public static int numOfNotes;
    public float noteTime;  //timestamp per la nota
    public float noteSpawnY;//coordinata di spawn verticale nota
    public float noteTapY;  //coordinata in cui e' interagibile la nota
    public float noteDespawnY //coordinata in cui viene rimossa la nota se non e' stata colpita
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY); //getter method che ritorna la distanza tra il punto di interazione e il punto di spawn
        }
    }
    public static MidiFile midiFile; //posizione del file MIDI in formato .mid
    public static float GetNoteScoreValueFromSong()
    {
        Debug.Log($"Valore nota: {ScoreManager._MAX_SCORE / numOfNotes}");
        float noteValue = (ScoreManager._MAX_SCORE / numOfNotes > 0) ? ScoreManager._MAX_SCORE / numOfNotes : 100f;
        return noteValue;
    }
    public void SetNoteTime()
    {
        noteTime = BPM / 60f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this; // istanzio il singleton
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite()); //nel caso sia build Web leggeremo da un indirizzo http(s), altrimenti da un file
            
        }
        else
        {
            ReadFromFile();
        }
    }

    private IEnumerator ReadFromWebsite()
    {
        using UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) //catch di errori di connessione o protocollo
        {
            Debug.LogError(www.error);
            throw new Exception("Errore di connessione");
        }
        else
        {
            //memorizzo lo stream di dati
            byte[] results = www.downloadHandler.data;
            using var stream = new MemoryStream(results);
            midiFile = MidiFile.Read(stream); //leggo il file .mid dallo stream
            GetDataFromMidi(); //parsing dei dati ottenuti
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi() //metodo per il parsing dei dati da un file .mid
    {
        var notes = midiFile.GetNotes(); //ricava i messaggi MIDI
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0); //le note vengono messe in un array
        numOfNotes = notes.Count;
        Debug.Log($"GetDataFromMidi()\nNumOfNotes: {numOfNotes}");
        scoreManager = ScoreManager.GetScoreManager();
        scoreManager.Invoke(nameof(scoreManager.CalcolaValoreNota), 0.2f);

        foreach (var lane in lanes) lane.SetTimeStamps(array); //inizializzo le Lanes con i vari timestamps delle note
        SetNoteTime();
        Invoke(nameof(StartSong), songDelayInSeconds); //richiamo il metodo per avviare la canzone
    }
    public void StartSong() //riproduce il file audio
    {
        audioSource.Play();
    }
    public static double GetAudioSourceTime() //ottiene il tempo della canzone dividendo Samples / Freq (Hz -> 1/s)
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

}