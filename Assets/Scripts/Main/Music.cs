//using UnityEngine Audio;
using UnityEngine;

public class Music : MonoBehaviour
{
    /*
        [SerializeField] AudioSource musicSource;
        //[SerializeField] AudioSource SFXSource;


        public AudioClip background;

        // Start is called before the first frame update
        private void Start()
        {
            musicSource.clip = background;
            musicSource.Play();
        }*/

    /*public void (AudioClip clip)
    {
        SFXSource.PlayOneShoot(clip);
    }*/
    private AudioSource Musica;
    private void Awake()
    {
        Musica = GetComponent<AudioSource>();
    }
    private void Start()
    {
        SuonaMusica();
    }
    public void SuonaMusica()
    {
        if (!Application.isPlaying)
        {
            if(Musica != null)
            {
                Musica.Play();
            }
        }
    }
     

}

