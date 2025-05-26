using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Movement movement;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            movement.onGround = true;
        }
    }
}