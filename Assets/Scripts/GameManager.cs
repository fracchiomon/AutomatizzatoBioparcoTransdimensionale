using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Creazione del Singleton del GameManager che si occupa della gestione delle transizione tra scene e meccaniche piu` importanti
/// </summary>
public class GameManager : MonoBehaviour
{
    protected static GameManager Instance;
    public bool HasStarted { get; private set; }
    public bool IsPlaying  { get; private set; }

    protected GameManager()
    { }

    public void SetHasStarted(bool hasStarted)
    {
        this.HasStarted = hasStarted;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;  
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public static GameManager GetInstance()
    {
        return Instance;
    }

    public static void ToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    public static void ToLevel1()
    {
        SceneManager.LoadScene(sceneName: "Level1");
    }
    public static void ToLevel2()
    {
        SceneManager.LoadScene(sceneName: "Level2");
    }
    public static void ToLevel3()
    {
        SceneManager.LoadScene(sceneName: "Level3");
    }
    public static void ToLevel4()
    {
        SceneManager.LoadScene(sceneName: "Level4");
    }
    public static void ToLevel5()
    {
        SceneManager.LoadScene(sceneName: "Level5");
    }

    public static void ExitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit(exitCode: 0);
        }
    }



}
