using System.Collections;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

/// <summary>
/// Singleton responsabile del parsing del file MIDI tramite Melanchall, associato alla canzone scelta; il file viene filtrato dalle Lanes (vedi Lane.cs) che si prendono la nota MIDI corrispondente, e si occupano di gestire le note e l'input da tastiera e Mouse. 
/// </summary>
public class SongManager : MonoBehaviour
{
    public static bool IsDebugEnabled;
    public bool nonStaticIsDebugEnabled;
    public float DEBUG_TIMESCALE;

    public static SongManager Instance { get; private set; } //per richiamare istanza di questo oggetto
    public ScoreManager scoreManager;
    public SongSelect SongSelection { get; private set; }
    private Song songSelected;

    public float songDelayInSeconds; //tempo tra una nota e l'altra
    public float marginOfError; // in seconds
    public int inputDelayInMilliseconds; //per controllare eventuale lag dell'input dell'utente (calibrazione)

    public AudioSource audioSource; //contiene la canzone
    public Lane[] lanes; //gestiscono le "corsie" sulle quali viaggeranno le note
    //TODO: implementare piu songs in classe Song
    public string fileLocation; //dove si trova la canzone
    public uint BPM;
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

    /// <summary>
    /// Nell'awake istanzio il singleton del SongManager e invoco la funzione opportuna per fare il parsing del MIDI file
    /// </summary>
    void Awake()
    {
        Instance = this; // istanzio il singleton
        IsDebugEnabled = nonStaticIsDebugEnabled;


    }
    private void Start()
    {
        Time.timeScale = 0; //start da tempo fermo, avvia quando selezionata canzone

        SongSelection = SongSelect.GetInstance();

        //GetSongFromSelection();
    }


    public void GetSongFromSelection()
    {
        try
        {
            songSelected = SongSelection.GetSongToLoad();
            fileLocation = songSelected.MIDI_SONG_PATH;
            print(fileLocation);

            this.audioSource = songSelected.songSource;
            print(audioSource.name);

            BPM = songSelected.SONG_TEMPO;
        }
        catch
        {
            print(new Exception());
        }


        TentaLetturaFile();
    }


    //------------DEBUG_SECTION-------------//
    public static void DEBUG_TIMESCALE_EDIT(float newTSvalue)
    {
        if (IsDebugEnabled)
            Time.timeScale = newTSvalue;
    }
    public static bool DEBUG_IS_PAUSED;
    public static void DEBUG_PAUSE()
    {
        if (IsDebugEnabled)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (DEBUG_IS_PAUSED)
                    DEBUG_TIMESCALE_EDIT(1);
                else
                    DEBUG_TIMESCALE_EDIT(0);
            }

        }

    }
    private void OnValidate()
    {
        IsDebugEnabled = nonStaticIsDebugEnabled;
        if (IsDebugEnabled)
            Time.timeScale = DEBUG_TIMESCALE;
    }
    //------------END_DEBUG_SECTION--------//



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
    /// Aggiorna il valore del punteggio di una nota singola dividendo il massimale MAX_SCORE sul numero di note presenti nella map. Nel caso MAX_SCORE fosse non inizializzato o negativo, assegna un valore default di 100.
    /// </summary>
    /// <returns></returns>
    public static float GetNoteScoreValueFromSong()
    {
        if (IsDebugEnabled)
            Debug.Log($"Valore nota: {ScoreManager._MAX_SCORE / numOfNotes}");
        float noteValue = (ScoreManager._MAX_SCORE / numOfNotes > 0) ? ScoreManager._MAX_SCORE / numOfNotes : 100f;
        return noteValue;
    }

    /// <summary>
    /// La funzione aggiorna il valore di noteTime dividendo i Battiti per minuto su 60 secondi, ottiene così un valore in secondi
    /// </summary>
    public void SetNoteTime()
    {
        noteTime = BPM / 60f;
    }


    /// <summary>
    /// Coroutine per lettura asincrona del MIDI file da un indirizzo http, memorizzato in uno stream di dati mandato alla funzione GetDataFromMidi()
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private IEnumerator ReadFromWebsite()
    {
        using UnityWebRequest www = UnityWebRequest.Get(fileLocation);
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

    /// <summary>
    /// Legge un file MIDI a partire dalla location /Assets/StreamingAssets
    /// </summary>
    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(fileLocation);
        GetDataFromMidi();
    }


    /// <summary>
    /// Effettua il parsing del file MIDI, usando la libreria DryWetMidi, memorizza le note ottenute da GetNotes() in un array, inizializza il punteggio singolo di una nota nello ScoreManager. Inoltre, assegna i timestamps relativi alle note a ciascuna Lane, le quali filtreranno quelle a loro assegnate; infine, avvia la canzone
    /// </summary>
    public void GetDataFromMidi() //methodo per il parsing dei dati da un file .mid
    {
        var notes = midiFile.GetNotes(); //ricava i messaggi MIDI
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0); //le note vengono messe in un array
        numOfNotes = notes.Count;
        if (IsDebugEnabled)
            Debug.Log($"GetDataFromMidi()\nNumOfNotes: {numOfNotes}");
        StartCoroutine(ScoreManager.CalcolaValoreNota());

        foreach (var lane in lanes) lane.SetTimeStamps(array); //inizializzo le Lanes con i vari timestamps delle note
        SetNoteTime();
        Invoke(nameof(StartSong), songDelayInSeconds); //richiamo il metodo per avviare la canzone
    }


    public void StartGame()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;

    }
    public void StartSong() //riproduce il file audio
    {
        audioSource.Play();
    }

    /// <summary>
    /// ottiene il tempo della canzone dividendo Samples / Freq (Hz -> 1/s)
    /// </summary>
    /// <returns></returns>
    public static float GetAudioSourceTime()
    {
        return (float)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

}