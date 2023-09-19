using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreForMiniGame : MonoBehaviour
{
    private static ScoreForMiniGame instance;
    private int highScore; 

    public static ScoreForMiniGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ScoreForMiniGame").AddComponent<ScoreForMiniGame>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public void SetHighScore(int score)
    {
        highScore = score;
    }

    public int GetHighScore()
    {
        return highScore; 
    }
}
