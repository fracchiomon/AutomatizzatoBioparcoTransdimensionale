using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// Creazione del Singleton del GameManager che si occupa della gestione delle transizione tra scene e meccaniche piu` importanti
/// </summary>
public class GiocoManager : MonoBehaviour
{
    protected static GiocoManager Instance;

    [SerializeField] private AudioMixerGroup MusicMixer, EFXMixer, MasterMixer;


    private bool HasStarted;
    public bool GetHasStarted()
    {
        return HasStarted;
    }
    public void SetHasStarted(bool start)
    {
        HasStarted = start;
    }


    protected GiocoManager()
    { }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }



    public static GiocoManager GetInstance()
    {
        return Instance;
    }

    public static void ToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    public static void OpenSettingsPanel()
    {

    }
    public static void ToLevel1()
    {
        SceneManager.LoadScene(sceneName: "Rhythmicon");
    }
    public static void ToLevel2()
    {
        SceneManager.LoadScene(sceneName: "NoteHunt");
    }
    public static void ToLevel3()
    {
        SceneManager.LoadScene(sceneName: "FindTheTime");
    }
    public static void ToLevel4()
    {
        SceneManager.LoadScene(sceneName: "CookingNotes");
    }
    public static void ToLevel5()
    {
        SceneManager.LoadScene(sceneName: "WhackANote");
    }

    public static void ExitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit(exitCode: 0);
        }
    }

    public static void RHYTHMICON_ConfermaTornaAlMenu()
    {
        ToMainMenu();

    }

    public void SETTINGS_MusicVolumeSlider(float volume)
    {
        MusicMixer.audioMixer.SetFloat("MUSIC_VOLUME", Mathf.Log10(volume) * 20);
    }

    public void SETTINGS_EFXVolumeSlider(float volume)
    {
        EFXMixer.audioMixer.SetFloat("EFX_VOLUME", Mathf.Log10(volume) * 20);
    }

    public void SETTINGS_MasterVolumeSlider(float volume)
    {
        MasterMixer.audioMixer.SetFloat("MASTER_VOLUME", Mathf.Log10(volume) * 20);
        MusicMixer.audioMixer.SetFloat("MUSIC_VOLUME", Mathf.Log10(volume) * 20);
        EFXMixer.audioMixer.SetFloat("EFX_VOLUME", Mathf.Log10(volume) * 20);
    }


}
