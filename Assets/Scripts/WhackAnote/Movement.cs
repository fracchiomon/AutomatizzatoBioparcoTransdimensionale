using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject[] moles;
    [SerializeField] private GameObject cron;
    [SerializeField] private GameObject hammer;
    [SerializeField] private String[] Notes;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private AudioClip[] suoniNote;
    [SerializeField] private TextMeshProUGUI rightNote;
    private Action<String, int> MoleMessage;
    private AudioSource noteSound;
    private bool notaSuonata;
    public Text gameText;
    public Text pointText;
    private float punteggioPartita;
    public int index = 0;
    int index1 = 0;



    private void Awake()
    {
        int sharedRandIndex = UnityEngine.Random.Range(0, Notes.Length - 1);
        RandomNote.SetRandomIndex(sharedRandIndex);
        rightNote.text = "Schiaccia la Nota " + Notes[sharedRandIndex];
    }

    // Start is called before the first frame update
    void Start()
    {

        punteggioPartita = 0;
        if(moles != null)
        {
            this.moles[index].transform.position = this.moles[index].GetComponent<MoleController>().Points[moles[index].GetComponent<MoleController>()._indexPoint].transform.position;
        }
        this.cron.GetComponent<Cronometro>().setGameTime(20.0f);
        noteSound = GetComponent<AudioSource>();
        this.MoleMessage = FindObjectOfType<UI_Message>().SpawnMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.moles[index].GetComponent<MoleController>().hasFinished == true)
        {
            index1 = UnityEngine.Random.Range(0, this.moles.Length);
            while (index1 == index)
            {
                index1 = UnityEngine.Random.Range(0, this.moles.Length);
            }
            index = index1;
        }
        this.cron.GetComponent<Cronometro>().timeRunsOut();
        if (this.cron.GetComponent<Cronometro>().getGameTime() < 0)
        {
            Debug.Log("Fine");
        }
        if (this.moles[index].GetComponent <MoleController>().isHitted && this.moles[index].GetComponent<MoleController>().GetIndexNote() == RandomNote.GetRandomIndex())
        {
            this.punteggioPartita += this.moles[index].GetComponent<MoleController>().GetPoint();
            this.moles[index].GetComponent<MoleController>().isHitted = false;
        }
        else if (this.moles[index].GetComponent<MoleController>().isHitted)
        {
            this.punteggioPartita -= this.moles[index].GetComponent<MoleController>().GetPoint();
            this.moles[index].GetComponent<MoleController>().isHitted = false;
        }
        gameText.text = this.cron.GetComponent<Cronometro>().getGameTime().ToString();
        pointText.text = punteggioPartita.ToString();
        if (this.moles[index].GetComponent<MoleController>()._indexPoint == 1 && !notaSuonata)
        {
            string notaSuon = Notes[this.moles[index].GetComponent<MoleController>().GetIndexNote()];
            this.MoleMessage(notaSuon, 1);
            notaSuonata = true;
            noteSound.clip = suoniNote[this.moles[index].GetComponent<MoleController>().GetIndexNote()];
            noteSound.Play();
        }
        if (this.moles[index].GetComponent<MoleController>()._indexPoint == 2)
        {
            notaSuonata = false;
            noteSound.Stop();
        }
        this.moles[index].GetComponent<MoleController>().Move();
        this.hammer.GetComponent<HammerController>().Move();
    }

    public void ToMainMenu()
    {
        ScreenFader.Instance.StartFadeToOpaque(
            (Action)(() =>
            {
                SceneManager.LoadScene(sceneName: "MainMenu");
                ScreenFader.Instance.StartFadeToTransparent(null);
            })
            );
    }



}
