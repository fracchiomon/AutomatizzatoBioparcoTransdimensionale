using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    Image fillableBar;
    [SerializeField] private float maxTime;
    [SerializeField] private int punteggioVincita;


    private static float timeLeft;

    //[SerializeField] private ScoreForMiniGame finalScore;


    void Start()
    {
        fillableBar = GetComponent<Image>();


        timeLeft = maxTime;
    }

    void Update()
    {

        timeLeft -= Time.deltaTime;
        fillableBar.fillAmount = timeLeft / maxTime;


        Debug.Log(UI_Punt.Punteggio());

        if (timeLeft < 0 && UI_Punt.Punteggio() < punteggioVincita)
        {
            Debug.Log("tempo finito");

            // Time.timeScale = 0;                     //quando la barra finisce termina il gioco
            ScoreForMiniGame.Instance.SetHighScore(UI_Punt.Punteggio());
            SceneManager.LoadScene(sceneName: "Lose");
        }
        else if (timeLeft < 0 && UI_Punt.Punteggio() >= punteggioVincita)
        {

            ScoreForMiniGame.Instance.SetHighScore(UI_Punt.Punteggio());

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SaveManager.Instance.bestNoteHunt = UI_Punt.Punteggio();

            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SaveManager.Instance.bestFindTheNote = UI_Punt.Punteggio();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SaveManager.Instance.bestWhackANote = UI_Punt.Punteggio();
            }

            SaveManager.Instance.Save();

            SceneManager.LoadScene(sceneName: "Victory");

            Debug.Log("tempo finito");

            // Time.timeScale = 0;
        }
    }
}
