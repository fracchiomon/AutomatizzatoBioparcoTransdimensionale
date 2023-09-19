using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class SaveLoad_JSON : MonoBehaviour
{
    //salva/carica i JSON convertendoli in Base64
    //classe saveManager (singleton)
    //si potrebbe fare un tasto nel mainMenu per stampare i best scores

    private const string saveFileName = "PentaNotes.save";

    //se ho più salvataggi posso fare il get con il numero del salvataggio
    private static string GetSaveFileName(int index)
    {
        return "Game_" + index + ".save";
    }

    private static string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/" + saveFileName;
    }



    //l'apply e il save verranno gestiti tramite codice quando una partita finisce/quando si apre il gioco

    /*// -----------------
    void Update() //<-- dovrei farlo su un altro obj e questa classe deve essere tutta statica
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(DoesSaveFileExist())
            {
                SaveData data = LoadFromFile();
                ApplySaveData(data);
            }
            else
            {
                Debug.LogWarning("No SaveData yet!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData data = GenerateSaveData();
            SaveToFile(data);
        }
    }
    // ------------------- */


    //per capire in che scena sono ho il SceneManager.GetActiveScene().buildIndex
    //da cui prendo i dati che mi servono

    //funzione che salva i dati
    public static void SaveToFile(SaveData data)
    {
        string jsonText = JsonUtility.ToJson(data);
        jsonText = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonText));
        File.WriteAllText( GetSaveFilePath(), jsonText );
    }

    //funzione che carica
    public static SaveData LoadFromFile()
    {
        string jsonText = File.ReadAllText( GetSaveFilePath() );
        jsonText = Encoding.UTF8.GetString(Convert.FromBase64String(jsonText));
        SaveData data = JsonUtility.FromJson<SaveData>(jsonText);
        return data;
    }

    //funzione che verifica se esiste un save data
    public static bool DoesSaveFileExist()
    {
        return File.Exists(GetSaveFilePath());
    }

    //prepara il file con lo stato del gioco
    public static SaveData GenerateSaveData()
    {
        // creiamo il save data
        SaveData data = new SaveData();
        // settiamo i vari punteggi gestiti dal SaveManager
        data.scoreRythmicon = SaveManager.Instance.bestRythmicon;
        data.scoreNoteHunt = SaveManager.Instance.bestNoteHunt;
        data.scoreCookingNotes = SaveManager.Instance.bestCookingNotes;
        data.scoreFindTheNote = SaveManager.Instance.bestFindTheNote;
        data.scoreWhackANote = SaveManager.Instance.bestWhackANote = data.scoreWhackANote;

        return data;
    }

    public static void ApplySaveData(SaveData data)
    {
        //assegno i punteggi salvati alla classe che li contiene
        SaveManager.Instance.bestRythmicon = data.scoreRythmicon;
        SaveManager.Instance.bestNoteHunt = data.scoreNoteHunt;
        SaveManager.Instance.bestCookingNotes = data.scoreCookingNotes;
        SaveManager.Instance.bestFindTheNote = data.scoreFindTheNote;
        SaveManager.Instance.bestWhackANote = data.scoreWhackANote;
    }
}
