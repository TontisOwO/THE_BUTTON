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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (movement.dashing)
        {
            if (other.CompareTag("Ground"))
            {
                movement.onGround = true;
            }
        }
    }
}