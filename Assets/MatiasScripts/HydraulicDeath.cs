using UnityEngine;
using UnityEngine.SceneManagement;

public class HydraulicDeath : MonoBehaviour
{
    AudioManager audioManager;
    Movement movement;
    public int damage = 100000;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        movement = GetComponent<Movement>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movement.TakeDamage(damage, this.GetComponent<Collider2D>());
            Debug.Log("Touched Player");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioManager.playSFX(audioManager.HudraulicHit);
        }
    }
}
