using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SO_Recipe[] _ricette;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI timeTxt;
    private Action<SO_Recipe> updateFrullatore;
    private Action<SO_NotaItem[]> updateDispensa;
    private int _score;
    private int recipeIndex;
    private Cronometro gameTimer;
    private bool isOnPlay;

    public int score
    {
        get
        {
            return this._score;
        }
        set
        {
            this._score = value;
            UpdateScore();
        }
    }
    public SO_Recipe[] ricette => this._ricette;

    private void Start()
    {
        this.gameTimer = FindObjectOfType<Cronometro>();
        this.updateFrullatore = FindObjectOfType<FrullatoreController>().UpdateRicetta;
        this.updateDispensa = FindObjectOfType<UI_windowDispensa>().UpdateIngredienti;
        this.recipeIndex = 1;

        this.isOnPlay = true;
        this.score = 500;
        this.gameTimer.setGameTime(300);
    }

    private void Update()
    {
        if(this.isOnPlay)
        {
            this.gameTimer.timeRunsOut();
            this.timeTxt.text = ((int) this.gameTimer.getGameTime()).ToString();
            /* //ad ogni minuto che passa, tolgo 100 punti
            if (((int) this.gameTimer.getGameTime()) % 60 == 0)
            {
                this.score -= 100;
            }*/

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

    public void StopPlay()
    {
        this.isOnPlay = false;
    }

    public void UpdateScore()
    {
        this.scoreTxt.text = this._score.ToString();
    }

    public void ReduceScore(int s)
    {
        this._score -= s;
        UpdateScore();
    }

    public void ToMainMenu()
    {
        StopPlay();
        ScreenFader.Instance.StartFadeToOpaque(
            (Action)(() =>
            {
                SceneManager.LoadScene(sceneName: "MainMenu");
                ScreenFader.Instance.StartFadeToTransparent(null);
            })
            );
    }

    public void RestartRicetta()
    {
        //passo allo startFadeToTransparent l'action da eseguire
        //quando ha completato il toOpaque
        ScreenFader.Instance.StartFadeToOpaque(
            (Action)(() =>
            {
                updateFrullatore(this._ricette[recipeIndex]);
                updateDispensa(this._ricette[recipeIndex].ingredienti);
                this.recipeIndex++;
                if (this.recipeIndex == this._ricette.Length)
                {
                    this.recipeIndex = 0;
                }
                //passo allo startFadeToTransparent l'action da eseguire
                //quando ha completato il toTransparent
                ScreenFader.Instance.StartFadeToTransparent(

                    (Action)( ()=>
                    {
                        this.score = 500;
                        this.isOnPlay = true;
                    })

                    );
            })
            );
    }
}
