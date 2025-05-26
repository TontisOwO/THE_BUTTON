using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HydraulicPress : MonoBehaviour
{
    public Collider2D deathCollider;
    public GameObject hydraulicPress;
    

    public float speed;
    public int length;
    public int offset;

    private void FixedUpdate()
    {
        HydraulicMovement();
    }
    
    void HydraulicMovement()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * length - offset;
        hydraulicPress.transform.position = new Vector3(0, y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(deathCollider)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //LoadScene("loseMenu")
            }
        }
    }
}
