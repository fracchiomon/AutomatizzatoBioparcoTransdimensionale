using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    Image fillableBar;
    [SerializeField] private float maxTime ;
    [SerializeField] private int punteggioVincita;

    private static float timeLeft;

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
      
        if (timeLeft < 0 && UI_Punt.Punteggio()<punteggioVincita)
        {
            Debug.Log("tempo finito");

           // Time.timeScale = 0;                     //quando la barra finisce termina il gioco

            SceneManager.LoadScene(sceneName: "Lose");
        }
        else if (timeLeft < 0 && UI_Punt.Punteggio() >= punteggioVincita)
        {
            SceneManager.LoadScene(sceneName: "Victory");

            Debug.Log("tempo finito");

           // Time.timeScale = 0;
        }
    }
}
