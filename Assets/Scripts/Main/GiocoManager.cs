using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

/// <summary>
/// Creazione del Singleton del GameManager che si occupa della gestione delle transizione tra scene e meccaniche piu` importanti
/// </summary>
public class GiocoManager : MonoBehaviour
{
    #region parte del Singleton
    protected static GiocoManager _instance;

    protected GiocoManager()
    { }

    public static GiocoManager Instance
    {
        get
        {
            //controllo se è registrato, perché poteri richiamare il get prima dell'awake
            if (_instance == null)
            {
                //lo cerco nella scena
                _instance = FindObjectOfType<GiocoManager>(true);

                //controllo se l'ho trovato
                if (_instance == null)
                {
                    //non è stato trovato e quindi lo creo
                    //Resources.Load("Game"); //--> gli passo il nome di un file "" che si trova nella cartella Resource
                    GameObject go = Instantiate<GameObject>(Resources.Load("Game") as GameObject);
                    _instance = go.GetComponent<GiocoManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    #endregion

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


    public void ToMainMenu()
    {
        ScreenFader.Instance.StartFadeToOpaque(
            (Action)(() =>
            {
                SceneManager.LoadScene(sceneName: "MainMenu");
                ScreenFader.Instance.StartFadeToTransparent(null);
            })
            );
    }
    public void OpenSettingsPanel()
    {

    }
    public void ToLevel1()
    {
        ScreenFader.Instance.StartFadeToOpaque(
           (Action)(() =>
           {
               SceneManager.LoadScene(sceneName: "Rhythmicon");
               ScreenFader.Instance.StartFadeToTransparent(null);
               Time.timeScale = 0;
           })
           );
    }
    public void ToLevel2()
    {
        ScreenFader.Instance.StartFadeToOpaque(
           (Action)(() =>
           {
               SceneManager.LoadScene(sceneName: "NoteHunt");
               ScreenFader.Instance.StartFadeToTransparent(null);
           })
           );
    }
    public void ToLevel3()
    {
        ScreenFader.Instance.StartFadeToOpaque(
           (Action)(() =>
           {
               SceneManager.LoadScene(sceneName: "FindTheTime");
               ScreenFader.Instance.StartFadeToTransparent(null);
           })
           );
    }
    public void ToLevel4()
    {
        ScreenFader.Instance.StartFadeToOpaque(
           (Action)(() =>
           {
               SceneManager.LoadScene(sceneName: "CookingNotes");
               ScreenFader.Instance.StartFadeToTransparent(null);
           })
           );
    }
    public void ToLevel5()
    {
        ScreenFader.Instance.StartFadeToOpaque(
           (Action)(() =>
           {
               SceneManager.LoadScene(sceneName: "WhackANote");
               ScreenFader.Instance.StartFadeToTransparent(null);
           })
           );
    }


    public void ExitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit(exitCode: 0);
        }
    }

    public void RHYTHMICON_ConfermaTornaAlMenu()
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
