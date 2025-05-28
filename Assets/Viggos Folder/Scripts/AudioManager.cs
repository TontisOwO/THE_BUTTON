using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource ambiance;
    [SerializeField] AudioSource hudraulic;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip Music;
    public AudioClip Ambiance;
    public AudioClip Hudraulic;
    public AudioClip Walking;
    public AudioClip Laser;
    public AudioClip HudraulicHit;
    public AudioClip Turret;
    public AudioClip BuffPickUp;

    private void Start()
    {
        musicSource.clip = Music;
        ambiance.clip = Ambiance;
        hudraulic.clip = Hudraulic;
        musicSource.Play();
        ambiance.Play();
        hudraulic.Play();
    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    //Copy this into the script where it's going to play the sound

    //Cach this: AudioManager audioManager;

    //private void Awake()
    //{
    //    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //}

    //Where the action and sound is going to be: audioManager.playSFX(audioManager.nameOfTheSound);
}
