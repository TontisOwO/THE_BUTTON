using UnityEngine;

public class StillTurret : MonoBehaviour
{
    public float FireRate;
    public float Force;
    public GameObject TurretHead;
    public GameObject Bullet;
    public Transform Shootpoint;

    float nextTimeToFire = 0;

    void FixedUpdate()
    {
        if (Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / FireRate;
            shoot();
        }
    }

    private void shoot()
    {
        GameObject newBullet = Instantiate(Bullet, Shootpoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().AddForce(Direction * Force, ForceMode2D.Force);
    }
}
