using UnityEngine;

public class NoteTest : MonoBehaviour
{
    double timeInstantiated; //timestamp relativo a quando e' istanziato l'oggetto
    public float assignedTime; //timestamp assegnato a quando "dovrebbe essere colpito"
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime(); //ottengo il timeInstantiated dal tempo attuale della canzone
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated; //quanto tempo e' passato dall'istanziazione? servira' per capire quando dovrebbe arrivare a destinazione
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2)); //???


        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}


