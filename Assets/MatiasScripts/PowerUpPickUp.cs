using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    public bool speedPickUp;
    public bool lowGravityPickUp;
    public bool speedPickUpPickedUp = false;
    public bool lowGravityPickedUp = false;

    private SpriteRenderer spriteRenderer;
    public GameObject movementScriptAcces;
    private Rigidbody2D pickUpRigidBody2D;
    private Collider2D pickUpCollider2D;
    public Rigidbody2D playerRigidyBody2D;

    public float speedBuffCountDown = 0;
    public float gravityBuffCountDown = 0;

    private void Awake()
    {
        pickUpCollider2D = GetComponent<Collider2D>();  
        spriteRenderer = GetComponent<SpriteRenderer>();    
        pickUpRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BuffRemoval();
        Debug.Log(speedBuffCountDown);
        Debug.Log(speedPickUpPickedUp);
        Debug.Log(lowGravityPickedUp);
        Debug.Log(gravityBuffCountDown);

       
        if (speedPickUpPickedUp == true)
        {
            speedBuffCountDown += Time.deltaTime;
        }

        if (lowGravityPickedUp == true)
        {
            gravityBuffCountDown += Time.deltaTime;
        }
    }

    void BuffRemoval()
    {
        if (speedBuffCountDown > 10 && speedPickUp == true)
        {
            movementScriptAcces.GetComponent<Movement>().movementSpeed -= 10;
            movementScriptAcces.GetComponent<Movement>().movementSpeedCap -= 10;

            speedBuffCountDown = 0;
            speedPickUpPickedUp = false;
        }

        if(gravityBuffCountDown > 10 && lowGravityPickUp == true)
        {
            playerRigidyBody2D.gravityScale = 1f;

            gravityBuffCountDown = 0;
            lowGravityPickedUp = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(spriteRenderer);
            Destroy(pickUpRigidBody2D);
            Destroy(pickUpCollider2D);
        }
        
        if(speedPickUp == true && collision.collider.CompareTag("Player"))
        {
            movementScriptAcces.GetComponent<Movement>().movementSpeed += 10;
            movementScriptAcces.GetComponent <Movement>().movementSpeedCap += 10;
            speedPickUpPickedUp = true;
        }

        if(lowGravityPickUp == true && collision.collider.CompareTag("Player"))
        {
            playerRigidyBody2D.gravityScale = 0.3f;
            lowGravityPickedUp = true;
        }
    }
}
