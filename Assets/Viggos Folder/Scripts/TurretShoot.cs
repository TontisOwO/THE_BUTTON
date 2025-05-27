using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    //[SerializeField] bulletSpeed;
    //[SerializeField] BulletDamage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
