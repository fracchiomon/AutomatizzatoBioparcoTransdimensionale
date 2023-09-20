using System;
using UnityEngine;


public class RandomNote : MonoBehaviour
{
    private static int randomNoteIndex;
    [SerializeField] public String[] Notes;


    public static int GetRandomIndex()
    {
        return randomNoteIndex;
    }

    public static void SetRandomIndex(int rand)
    {
        randomNoteIndex = rand;
        //Debug.Log(rand);
    } 

}
