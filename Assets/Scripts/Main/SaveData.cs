using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable] // <- permette di salvare la classe su file

public class SaveData
{
    //vengono salvate soltanto le variabili publiche e le [SerializeField] non private
    //vengono salvati anche oggetti di tipo [Serializable]
    public int scoreRythmicon;
    public int scoreNoteHunt;
    public int scoreCookingNotes;
    public int scoreFindTheNote;
    public int scoreWhackANote;

    public SaveData(/*int scoreRythmicon, int scoreNoteHunt, int scoreCookingNotes, int scoreFindTheNote, int */)
    {
        this.scoreRythmicon = 0;
        this.scoreNoteHunt = 0;
        this.scoreCookingNotes = 0;
        this.scoreFindTheNote = 0;
        this.scoreWhackANote = 0;
    }

}

/*
in JSON:
{
    "scoreRythmicon": ...,
    ... ,
    ... ,

    "score ... ": ...
}
 */
