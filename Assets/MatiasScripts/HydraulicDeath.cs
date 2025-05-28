using UnityEngine;

public class HydraulicDeath : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //LoadScene("loseMenu")
            Debug.Log("Touched Player");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioManager.playSFX(audioManager.HudraulicHit);
        }
    }
}
