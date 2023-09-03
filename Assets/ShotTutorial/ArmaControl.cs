using UnityEngine;


public class ArmaControl : MonoBehaviour
{

    //Variabili pubbliche
    public float fireRate = 0.5f; //Tempo tra un colpo e l'altro   
    public bool recargeOnMouseUp=false; //Se true, quando si rilascia il tasto di fuoco ricarica la raffica senza attesa
    public int maxAmmoRaffica = 5; //Colpi di una singola raffica
    public float pauseTime; //Tempo di attesa tra una raffica e l'altra
    public int ammoQuantity = 20; //Quantità di munizioni totali
    public GameObject proiettilePrefab;//Il prefab del proiettile
    public GameObject flashEffectPrefab;//Il prefab dell'effetto flash/fiammata
    public Transform shotPoint; //Il trasform che identifica il punto di creazione dei proiettili
    public AudioClip shotSound; //Il suono dello sparo



    //Variabili private
    int ammoRaffica; //Munizioni attuali
    float nextFire; //Prossimo colpo pronto
    float pauseTimer; //Il timer della pausa
    float cooldown; //Cantatore di colpi
    bool pause =false; //In pausa
    AudioSource audioSource; //L'audioSource che eseguirà il suono



    void Start()
    {
           AudioSourceCreation();//Crea l'AudioSource (se non presente)
           ammoRaffica = maxAmmoRaffica; //All'inizio il caricatore è pieno
    }

    //Creiamo l'AudioSource a runtime, senza doverlo inserire manualmente
    void AudioSourceCreation() {
        //Se non è stato creato manualmente
        if (!GetComponent<AudioSource>())
        {
            audioSource = gameObject.AddComponent<AudioSource>(); //Crea l'AdudioSource
        }
        else
        {
            audioSource = GetComponent<AudioSource>(); //Se già esisteva usa quello esistente
        }

        //Notare che si può creare l'audioSource anche manualmente, in modo da impostare i parametri 
        //i parametri (come il volume, il pitch ed altri) direttamente da Inspector
        //In alternativa potete anche impostarli qui, nel momento della creazione, scegliendo i parametri 
        //a runtime (audioSource.volume=1,  audioSource.pitch=1, ecc.)

    }



    void Update()
    {
        if (!pause && ammoQuantity>0) //Se è tra una raffica e l'altra non sparare
        {
            if (fireRate == 0 && Input.GetButtonDown("Fire1"))
            {   //Il primo colpo lo effettua senza dover attendere nulla
                Shoot();
            }
            else
            {
                if (Input.GetButton("Fire1") && Time.time > nextFire && fireRate > 0) //Pronto per sparare
                {
                    //Colpi successivi, tenendo premuto il pulsante
                    if (ammoRaffica > 0)
                    {   //Se ci sono munizioni
                        nextFire = Time.time + fireRate;
                        Shoot(); //Esegue la funzione di "sparo"

                    }
                    if (ammoRaffica == 0)
                    {   //Se non ci sono più munizioni
                        if (cooldown > Time.time)
                        {   //Se non è passato il tempo giusto
                            cooldown = Time.time + fireRate;
                        }
                    }
                }
            }

            if (Time.time > cooldown && ammoRaffica == 0)
            {  //Se il tempo di recupero (cooldown) è finito e le munizioni sono terminate
                pause = true;
                
            }
        }

   
        //Se si è scelto di far ricaricare al rilascio del tasto
       if(recargeOnMouseUp && Input.GetButtonUp("Fire1")){
            pauseTimer = 0;
            pause = false;


            if (ammoQuantity >= maxAmmoRaffica)//Se ci sono abbastanza i proiettili
                ammoRaffica = maxAmmoRaffica; //Ricarico per la prossima raffica
            else
                ammoRaffica = ammoQuantity; //Se non sono i proiettili metti quelli disponibili
        }

           //Se in pausa, fai il conteggio del tempo tra una raffica e l'altra
            if (pause)
            ShootPause();

    } //Fine di Update()


    //Metodo che fa il conteggio del tempo tra una raffica e l'altra
    void ShootPause()
    {
        pauseTimer += Time.deltaTime;
        if (pauseTimer >= pauseTime)
        {
            pauseTimer = 0;
            pause = false;


            if(ammoQuantity>= maxAmmoRaffica)//Se ci sono abbastanza i proiettili
            ammoRaffica = maxAmmoRaffica; //Ricarico per la prossima raffica
            else
            ammoRaffica = ammoQuantity; //Se non sono i proiettili metti quelli disponibili
        }
    }



    void Shoot() {

        ammoRaffica--; //Rimuove un colpo della raffica ad ogni sparo
        ammoQuantity--; //Rimuove un colpo dalla quantità in possesso

        //Istanzia l'oggetto proiettile, creando una copia del proiettilePrefab impostando la posizione e la rotazione
        GameObject proiettile = Instantiate(proiettilePrefab, shotPoint.position, shotPoint.rotation);
        //Notare che impostiamo la posizione di origine e la rotazione inziale uguali a quelle del trasform shotPoint 

        //Secondo istanziamento di un gameObject, necessario per l'effetto "fiammata"
        GameObject flashEffect = Instantiate(flashEffectPrefab, shotPoint.position, shotPoint.rotation); 
        //Notare che si potrebbe usare anche un'altra tecnica, meno dispendiosa in termini di prestazioni
        //che vedremo in seguito


        if (shotSound)//Assicuriamoci che c'è un suono scelto per il rumore dello sparo
        audioSource.PlayOneShot(shotSound); //Esegui il suono


        print("Shooooot");//Spara!
    }


 

} //Chiusura classe




