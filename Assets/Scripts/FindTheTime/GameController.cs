using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite bgImage;

    //public Sprite[] puzzles;
    public Sprite[] notes;
    public Sprite[] duration;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses, countCorrectGuesses, gameGuesses;

    private int firstGuessIndex, secondGuessIndex; 

    private string firstGuessPuzzle, secondGuessPuzzle;

    private bool firstAnim1, firstAnim2;

    [SerializeField] private GameObject[] monsters;
    [SerializeField] private Animator[] movements;
    [SerializeField] private Animator[] cardRotation; 


    private void Awake() // caricamento sprite note e durate 
    {
        notes = Resources.LoadAll<Sprite>("Sprite/Notes");
        duration = Resources.LoadAll<Sprite>("Sprite/Duration");


    }


    // Start is called before the first frame update
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        ShuffleList(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2; // quante serve indovinare 
        setAnimation();

        //movements[0].SetTrigger("from idle to correct");
    }

    void setAnimation()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
          
            movements[i] = monsters[i].GetComponent<Animator>();
            Debug.Log("entro");
        }
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for( int i = 0; i < objects.Length; i++ )
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
            cardRotation[i] = btns[i].GetComponent<Animator>();
        }
    }

   
    void AddGamePuzzles()
    {
        int looper = btns.Count; // num tot bottoni 
        ShuffleVect(notes);
        for ( int i = 0; i < looper/2; i++)
        {
            gamePuzzles.Add(notes[i]);
            string tempName= notes[i].name;

            for(int j = 0; j < duration.Length; j++)
            {
                if (tempName == duration[j].name)
                    gamePuzzles.Add(duration[j]);
            }
 
        }
    }


    void AddListeners()
    {
        foreach( Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle()); 
        }
    }




    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if ( !firstGuess ) // if tuch a button
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            if (!firstAnim1)
            {
                cardRotation[firstGuessIndex].SetTrigger("MostraCarta");
                //cardRotation[secondGuessIndex].SetTrigger("ToIdle");
                firstAnim1 = true;
            }

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

        }

        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            if (!firstAnim2)
            {
                cardRotation[secondGuessIndex].SetTrigger("MostraCarta");
                //cardRotation[secondGuessIndex].SetTrigger("ToIdle");
                firstAnim2 = true;
            }
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            //if(firstGuessPuzzle == secondGuessPuzzle)
            //{

            //    Debug.Log("the puzzle match");
            //}
            //else
            //{
            //    Debug.Log("the puzzle don't match");
            //}

            StartCoroutine(CheckIfThePuzzlesMatch());
        }

    }


    IEnumerator CheckIfThePuzzlesMatch()
    {
        bool correct = true;
        firstAnim1 = false;
        firstAnim2 = false;

        yield return new WaitForSeconds(1f);

        cardRotation[firstGuessIndex].SetTrigger("ToIdle");
        cardRotation[firstGuessIndex].SetTrigger("ToIdle");

        if ( firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false; // non si possono più toccare
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color  = new Color(0 , 0 , 0, 0); // non si vedono più
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            for (int i = 0; i < monsters.Length; i++)
            {

                movements[i].SetTrigger("from idle to correct");
            }

            UI_Punt.UpdateScore(5);

            CheckIfTheGameIsFinished();
        }
        else
        {
            correct = false; 
            yield return new WaitForSeconds(.5f); // aspetto .5f secondi che l'immagine sia visibile

            //cardRotation[firstGuessIndex].SetTrigger("NascondiCarta");
            //cardRotation[secondGuessIndex].SetTrigger("NascondiCarta");
            //cardRotation[firstGuessIndex].SetTrigger("ToIdle");
            //cardRotation[secondGuessIndex].SetTrigger("ToIdle");

            btns[firstGuessIndex].image.sprite = bgImage; // ritorna immagine del dietro
            btns[secondGuessIndex].image.sprite = bgImage;

            for (int i = 0; i < monsters.Length; i++)
            {

                movements[i].SetTrigger("from idle to wrong");

            }
        }

        yield return new WaitForSeconds(.5f);
        if ( correct)
        {
            for (int i = 0; i < monsters.Length; i++)
            {

                movements[i].SetTrigger("from correct to idle");

            }
        }
        else
        {
            for (int i = 0; i < monsters.Length; i++)
            {

                movements[i].SetTrigger("from wrong to idle");

            }
        }

        firstGuess = false; 
        secondGuess = false;
    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            SceneManager.LoadScene(sceneName: "Victory");
            Debug.Log("Game finished");
            Debug.Log("It took you" + countGuesses + " many guesses to finish the game");
        }
    }

    //mischia le carte 
    void ShuffleList(List<Sprite> list)
    {
        for ( int i = 0; i < list.Count; i++ )
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp; 
        }
    }

    void ShuffleVect(Sprite[] vect)
    {
        for ( int i = 0; i < vect.Length; i++)
        {
            Sprite temp = vect[i];
            int randomIndex = Random.Range(i, vect.Length);
            vect[i] = vect[randomIndex];
            vect[randomIndex] = temp; 
        }
    }


}
