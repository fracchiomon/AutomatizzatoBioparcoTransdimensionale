
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SO_Recipe[] _ricette;
    //[SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private int inGameTime;
    private Action<SO_Recipe> updateFrullatore;
    private Action<SO_NotaItem[]> updateDispensa;
    private float _score;
    private int totScore;
    //private List<int> totScore = new List<int>();
    private int recipeIndex;
    private Cronometro gameTimer;
    private bool _isOnPlay;

    public bool isOnPlay
    {
        get
        {
            return this._isOnPlay;
        }
        set
        {
            this._isOnPlay = value;
        }
    }

    public int score
    {
        get
        {
            return (int) this._score;
        }
        set
        {
            this._score = value;
            //UpdateScore();
        }
    }
    public SO_Recipe[] ricette => this._ricette;

    private void Start()
    {
        this.gameTimer = FindObjectOfType<Cronometro>();
        this.updateFrullatore = FindObjectOfType<FrullatoreController>().UpdateRicetta;
        this.updateDispensa = FindObjectOfType<UI_windowDispensa>().UpdateIngredienti;
        this.recipeIndex = 0;
        this.score = 0;
        this.totScore = 0;
        this._isOnPlay = true;
        this.gameTimer.setGameTime(this.inGameTime);
    }

    private void Update()
    {
        //Debug.Log(this.score);
        if(this._isOnPlay)
        {
            this.gameTimer.timeRunsOut();
            this.timeTxt.text = ((int) this.gameTimer.getGameTime()).ToString();

            //se il tempo arriva a zero, il giocatore perde
            if ((int) this.gameTimer.getGameTime() == 0)
            {
                ScreenFader.Instance.StartFadeToOpaque(

                    (Action)(() =>
                    {
                        SceneManager.LoadScene(sceneName: "Lose");
                        ScreenFader.Instance.StartFadeToTransparent(null);
                    })

                    );
            }
        }

    }

    /*public void StopPlay()
    {
        this._isOnPlay = false;
        this.timeTxt.text = "";
    }*/

    /*public void UpdateScore()
    {
        this.scoreTxt.text = this._score.ToString();
    }*/

    public void ReduceScore(float s)
    {
        this._score -= s;
        //UpdateScore();
    }

    public void ToMainMenu()
    {
        this._isOnPlay = false;
        ScreenFader.Instance.StartFadeToOpaque(
            (Action)(() =>
            {
                SceneManager.LoadScene(sceneName: "MainMenu");
                ScreenFader.Instance.StartFadeToTransparent(null);
            })
            );
    }

    public int CalcolaPunteggio()
    {
        int gameTime = (int) this.gameTimer.getGameTime();
        this.score += 2 * gameTime;
        //se il punteggio diventa negativo, lo setto a 0
        if(this.score <= 0)
        {
            this.score = 0;
        }

        this.totScore += this.score;

        return this.score;
    }

    public void RestartRicetta()
    {
        //passo allo startFadeToTransparent l'action da eseguire
        //quando ha completato il toOpaque
        ScreenFader.Instance.StartFadeToOpaque(
            (Action)(() =>
            {
                if(this.recipeIndex < this._ricette.Length - 1)
                {
                    this.recipeIndex++;
                    updateFrullatore(this._ricette[recipeIndex]);
                    updateDispensa(this._ricette[recipeIndex].ingredienti);
                }
                else
                {
                    SaveManager.Instance.bestCookingNotes = this.totScore;
                    SaveManager.Instance.Save();
                    ScoreForMiniGame.Instance.SetHighScore(this.totScore);
                    //this.recipeIndex = 0;
                    SceneManager.LoadScene(sceneName: "Victory");
                }
                //passo allo startFadeToTransparent l'action da eseguire
                //quando ha completato il toTransparent
                ScreenFader.Instance.StartFadeToTransparent(

                    (Action)( ()=>
                    {
                        this.score = 0;
                        this.gameTimer.setGameTime(this.inGameTime);
                        this._isOnPlay = true;
                    })

                    );
            })
            );
    }
}
