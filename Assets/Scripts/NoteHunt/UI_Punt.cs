//using UnityEngine;
//using UnityEngine.UI;

//public class UI_Punt : MonoBehaviour
//{
//    [SerializeField] private Text scoreText;
//    private static int currentScore;
    

//     void Start()
//    {
//        currentScore = 0;
//        //scoreText.text = currentScore.ToString() + " POINTS";
       
//    }

//    public static void UpdateScore(int addedValue)
//    {
//        currentScore += addedValue;                                 //per aggiornare il punteggio ogni volta che la nota viene distrutta = +5 punteggio
//        Debug.Log("Score: " + currentScore);
//    }

//    public void Update()
//    {
//        scoreText.text = currentScore.ToString() + " POINTS";
//    }

//    public static int Punteggio()
//    {
//        return currentScore;
//    }
//}
