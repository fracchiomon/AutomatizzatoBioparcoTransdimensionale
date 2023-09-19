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

    private const string saveFileName = "Game.save";

    //se ho più salvataggi posso fare il get con il numero del salvataggio
    private static string GetSaveFileName(int index)
    {
        return "Game_" + index + ".save";
    }

    private static string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/" + saveFileName;
    }


    // -----------------
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
    // -------------------


    //per capire in che scena sono ho il SceneManager.GetActiveScene().buildIndex
    //da cui prendo i dati che mi servono

    //funzione che salva i dati
    void SaveToFile(SaveData data)
    {
        string jsonText = JsonUtility.ToJson(data);
        jsonText = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonText));
        File.WriteAllText( GetSaveFilePath(), jsonText );
    }

    //funzione che carica
    SaveData LoadFromFile()
    {
        string jsonText = File.ReadAllText( GetSaveFilePath() );
        jsonText = Encoding.UTF8.GetString(Convert.FromBase64String(jsonText));
        SaveData data = JsonUtility.FromJson<SaveData>(jsonText);
        return data;
    }

    //funzione che verifica se esiste un save data
    bool DoesSaveFileExist()
    {
        return File.Exists(GetSaveFilePath());
    }

    //prepara il file con lo stato del gioco
    SaveData GenerateSaveData()
    {
        // creiamo il save data
        SaveData data = new SaveData();
        //data.scoreRythmicon = --> fare un get del punteggio
        return data;
    }

    void ApplySaveData(SaveData data)
    {
        //assegno i punteggi salvati alla classe che li contiene
    }
}
