using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SongSelect : MonoBehaviour
{
    protected static SongSelect Instance;

    public string SongName { get; set; }
    public uint BeatsPerMinute { get; set; }

    public List<string> MIDI_Map_FilePaths;
    public static AudioClip songToLoad;
    public List<AudioClip> Songs;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            Instance = this;
        }
        Songs = new List<AudioClip>();
        MIDI_Map_FilePaths = new List<string>();

    }

    public static AudioClip GetSongToLoad()
    {
        return songToLoad;
    }
    public static void SetSongToLoad(AudioClip song)
    {
        songToLoad = song;
    }

}
