using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource music;

    private void Awake()
    {
        this.music = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {
        if (!Application.isPlaying)
            music.Play();
        else
            music.Stop();
    }

}

