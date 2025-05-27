using UnityEngine;

enum LookDirection
{
    north,
    south,
    west,
    northwest,
    northeast,
    southwest,
    southeast
}

public class Movement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float stopSpeed;
    [SerializeField] float movementSpeedCap = 4;
    [SerializeField] float jumpForce;
    [SerializeField] float scaleFactor = 8;
    Rigidbody2D myRigidbody;
    Vector2 velocity;
    bool jumpStart;
    bool dashing;
    float jumpTime;
    Vector2 scale;
    public bool onGround;
    LookDirection lookDirection;
    public int HP;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        scale = transform.localScale;
        velocity = transform.position;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && myRigidbody.linearVelocityX >= -movementSpeedCap)
        {
            myRigidbody.linearVelocityX -= movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && myRigidbody.linearVelocityX <= movementSpeedCap)
        {
            myRigidbody.linearVelocityX += movementSpeed * Time.deltaTime;
        }

        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            switch (myRigidbody.linearVelocityX)
            {
                case < 0:
                    myRigidbody.linearVelocityX += stopSpeed * Time.deltaTime;
                    break;
                case > 0:
                    myRigidbody.linearVelocityX -= stopSpeed * Time.deltaTime;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jumpStart = true;
            myRigidbody.linearVelocityY += jumpForce;
            onGround = false;
        }
        if (jumpStart)
        {
            jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) && jumpStart)
        {
            jumpStart = false;
            switch (jumpTime)
            {
                case < 0.1f:
                    myRigidbody.linearVelocityY -= jumpForce * 0.5f;
                    break;

                case < 0.25f:
                    myRigidbody.linearVelocityY -= jumpForce * 0.3f;
                    break;
                
                default:

                    break;
            }
            jumpTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Q))
        {
            dashing = true;
        }
        if (dashing)
        {
            switch (lookDirection)
            {

            }
        }
        scale.x = 1 + (Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityX) + 1) - Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityY) + 1))/scaleFactor;
        scale.y = 1 + (Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityY) + 1) - Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityX) + 1))/scaleFactor;
        transform.localScale = scale;
    }
}
