using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    Image fillableBar;
    [SerializeField] private float maxTime;
    private float timeLeft;

    [SerializeField] private int punteggioVincita;
    //[SerializeField] private ScoreForMiniGame finalScore;
    private static int currentScore;
    [SerializeField] private Text scoreText;



    void Start()
    {

        fillableBar = GetComponent<Image>();
        timeLeft = maxTime;

        currentScore = 0;

    }


    public void GameTimerUpdate()
    {

        timeLeft -= Time.deltaTime;
        fillableBar.fillAmount = timeLeft / maxTime;

        scoreText.text = currentScore.ToString() + " POINTS";

        if (timeLeft < 0 && currentScore < punteggioVincita)
        {
          
            ScoreForMiniGame.Instance.SetHighScore(currentScore);
            SceneManager.LoadScene(sceneName: "Lose");

        }

        else if (timeLeft < 0 && currentScore >= punteggioVincita)
        {

            ScoreForMiniGame.Instance.SetHighScore(currentScore);

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SaveManager.Instance.bestNoteHunt = currentScore;

            }

            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SaveManager.Instance.bestFindTheNote = currentScore;
            }

            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SaveManager.Instance.bestWhackANote = currentScore;
            }

            SaveManager.Instance.Save();

            SceneManager.LoadScene(sceneName: "Victory");

            Debug.Log("tempo finito");

         
        }
    }

    public static void UpdateScore(int addedValue)
    {
        currentScore += addedValue;                                 //per aggiornare il punteggio ogni volta che la nota viene distrutta = +5 punteggio
        Debug.Log("Score: " + currentScore);
        
    }

    public float GetTimeLeft()
    {
        return timeLeft; 
    }
}

