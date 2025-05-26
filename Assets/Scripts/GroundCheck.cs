using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Movement movement;
    void OnCollisionEnter2D(Collision2D other)
    {
        movement.onGround = true;
    }
}
