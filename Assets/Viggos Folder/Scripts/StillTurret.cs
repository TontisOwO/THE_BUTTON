using UnityEngine;

public class TurretShootsStraight : MonoBehaviour
{
    public float FireRate;
    public float Force;
    public GameObject Bullet;
    public Transform Shootpoint;

    float nextTimeToFire = 0f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if (Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / FireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        audioManager.playSFX(audioManager.Turret);
        GameObject newBullet = Instantiate(Bullet, Shootpoint.position, Shootpoint.rotation);
        Vector2 direction = Shootpoint.right;

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * Force, ForceMode2D.Force);
        }
    }
}