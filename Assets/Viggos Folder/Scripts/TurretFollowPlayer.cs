using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretFollowPlayer : MonoBehaviour
{
    public float DetectionRange;
    public float FireRate;
    public float Force;
    public Transform PlayerTransform;
    public GameObject TurretHead;
    public GameObject Bullet;
    public GameObject AlarmLight;
    public Transform Shootpoint;

    public bool playerDetected;

    Vector2 Direction;

    float nextTimeToFire = 0;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        Vector2 targetPos = PlayerTransform.position;
        Direction = targetPos - (Vector2)transform.position;

        DetectPlayer();

        if (playerDetected)
        {
            AlarmLight.GetComponent<SpriteRenderer>().color = Color.red;
            Vector3 Look = transform.InverseTransformPoint(PlayerTransform.transform.position);
            float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg + 270;
            transform.Rotate(0, 0, Angle);

            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();

            }
        }
    }

    private void shoot()
    {
        audioManager.playSFX(audioManager.Turret);
        GameObject newBullet = Instantiate(Bullet, Shootpoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().AddForce(Direction * Force, ForceMode2D.Force);
    }

    private void DetectPlayer()
    {
        //is player within sight range?
        if (Vector2.Distance(transform.position, PlayerTransform.position) <= DetectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
            AlarmLight.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}