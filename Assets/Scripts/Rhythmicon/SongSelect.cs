using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SongSelect : MonoBehaviour
{
    protected static SongSelect Instance;

    public string SongName { get; set; }
    public string SongArtist { get; set; }
    public uint BeatsPerMinute { get; set; }

    //public List<string> MIDI_Map_FilePaths;
    public Song songToLoad;
    public List<Song> Songs;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            Instance = this;
        }
        //Songs = new List<AudioClip>();
        //MIDI_Map_FilePaths = new List<string>();

    }
    public static SongSelect GetInstance() { return Instance; }
    public Song GetSongToLoad()
    {
        return songToLoad;
    }
    public void SetSongToLoad(Song song)
    {
        songToLoad = song;
        SongName = songToLoad.SONG_NAME;
        SongArtist = songToLoad.SONG_ARTIST;
        BeatsPerMinute = songToLoad.SONG_TEMPO;
    }



}
