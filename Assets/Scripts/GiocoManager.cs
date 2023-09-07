using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Creazione del Singleton del GameManager che si occupa della gestione delle transizione tra scene e meccaniche piu` importanti
/// </summary>
public class GiocoManager : MonoBehaviour
{
    protected static GiocoManager Instance;
    public bool HasStarted { get; private set; }
    public bool IsPlaying { get; private set; }

    protected GiocoManager()
    { }

    public void SetHasStarted(bool hasStarted)
    {
        this.HasStarted = hasStarted;
    }

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
    public static void ToLevel1()
    {
        SceneManager.LoadScene(sceneName: "Poerio");

    }
    public static void ToLevel2()
    {
        SceneManager.LoadScene(sceneName: "Arianna");
    }
    public static void ToLevel3()
    {
        SceneManager.LoadScene(sceneName: "Martina");
    }
    public static void ToLevel4()
    {
        SceneManager.LoadScene(sceneName: "Nicholas");
    }
    public static void ToLevel5()
    {
        SceneManager.LoadScene(sceneName: "Lucrezia");
    }

    public static void ExitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit(exitCode: 0);
        }
    }



}
