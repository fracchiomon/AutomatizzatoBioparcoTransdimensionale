using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SongSelect : MonoBehaviour
{
    protected static SongSelect Instance;

    public string SongName { get; private set; }
    public uint BeatsPerMinute { get; private set; }

    public List<string> MIDI_Map_FilePaths;
    public AudioClip songToLoad;
    public List<AudioClip> Songs;
    [SerializeField] private Dictionary<string, AudioClip> canzoncine;

    public Dictionary<string, AudioClip> GetSongs()
    {
        return canzoncine;
    }

    public void SetSongs(string name, AudioClip clip)
    {
        canzoncine.Add(name, clip);
    }

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
        canzoncine = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in Songs)
        {
            canzoncine.Add(clip.name, clip);
        }
    }

}
