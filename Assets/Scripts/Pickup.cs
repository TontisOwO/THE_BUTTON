using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Vector3 height;
    [SerializeField] Quaternion rotation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("HeldMirror"))
        {
            collision.transform.GetComponent<BoxCollider2D>().isTrigger = true;
            rotation = collision.transform.rotation;
            collision.transform.SetParent(transform);
            collision.transform.SetPositionAndRotation(height + transform.position, rotation);
        }
    }
}