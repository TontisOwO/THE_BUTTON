using UnityEngine;
using System.Collections;

enum LookDirection
{
    north,
    south,
    west,
    east,
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
    [SerializeField] float dashStrength = 200;
    [SerializeField] float dashStopSpeed;
    [SerializeField] float dashEnd;
    [SerializeField] int HP;
    Rigidbody2D myRigidbody;
    Vector2 velocity;
    bool jumpStart;
    bool dashing;
    float jumpTime;
    float dashTime;
    Vector2 scale;
    public bool onGround;
    LookDirection lookDirection;

    bool isKnockback;
    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackDuration;

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
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Q))
        {
            dashing = true;
            switch (lookDirection)
            {
                case LookDirection.east:
                    if (myRigidbody.linearVelocityX < 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }
                    myRigidbody.linearVelocityX += dashStrength;
                    break;

                case LookDirection.west:
                    if (myRigidbody.linearVelocityX > 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }
                    myRigidbody.linearVelocityX -= dashStrength;
                    break;

                case LookDirection.north:
                    if (myRigidbody.linearVelocityY < 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }
                    myRigidbody.linearVelocityY += dashStrength;
                    break;

                case LookDirection.south:
                    if (myRigidbody.linearVelocityY > 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }
                    myRigidbody.linearVelocityY -= dashStrength;
                    break;

                case LookDirection.southeast:
                    if (myRigidbody.linearVelocityX < 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }
                    if (myRigidbody.linearVelocityY > 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }
                    myRigidbody.linearVelocityX += dashStrength * Mathf.Sqrt(2) / 2;
                    myRigidbody.linearVelocityY -= dashStrength * Mathf.Sqrt(2) / 2;
                    break;

                case LookDirection.northeast:
                    if (myRigidbody.linearVelocityX < 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }
                    if (myRigidbody.linearVelocityY < 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }
                    myRigidbody.linearVelocityX += dashStrength * Mathf.Sqrt(2) / 2;
                    myRigidbody.linearVelocityY += dashStrength * Mathf.Sqrt(2) / 2;
                    break;

                case LookDirection.southwest:
                    if (myRigidbody.linearVelocityX > 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }
                    if (myRigidbody.linearVelocityY > 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }
                    myRigidbody.linearVelocityX -= dashStrength * Mathf.Sqrt(2) / 2;
                    myRigidbody.linearVelocityY -= dashStrength * Mathf.Sqrt(2) / 2;
                    break;

                case LookDirection.northwest:
                    if (myRigidbody.linearVelocityX > 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }
                    if (myRigidbody.linearVelocityY < 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }
                    myRigidbody.linearVelocityX -= dashStrength * Mathf.Sqrt(2) / 2;
                    myRigidbody.linearVelocityY += dashStrength * Mathf.Sqrt(2) / 2;
                    break;
            }
        }
        lookDirection = GetLookDirection();
        if (dashing)
        {
            dashTime += Time.deltaTime;
        }
        if (dashTime >= dashEnd)
        {
            switch (myRigidbody.linearVelocityX)
            {
                case < 0:
                    myRigidbody.linearVelocityX += dashStopSpeed * Time.deltaTime;
                    break;
                case > 0:
                    myRigidbody.linearVelocityX -= dashStopSpeed * Time.deltaTime;
                    break;
            }
            switch (myRigidbody.linearVelocityY)
            {
                case < 0:
                    myRigidbody.linearVelocityY += dashStopSpeed * Time.deltaTime;
                    break;
                case > 0:
                    myRigidbody.linearVelocityY -= dashStopSpeed * Time.deltaTime;
                    break;
            }
            if (Mathf.Abs(myRigidbody.linearVelocityY) <= 1 && Mathf.Abs(myRigidbody.linearVelocityX) <= 1)
            {
                dashTime = 0;
            }
            dashing = false;
        }
        scale.x = 1 + (Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityX) + 1) - Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityY) + 1))/scaleFactor;
        scale.y = 1 + (Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityY) + 1) - Mathf.Log10(Mathf.Abs(myRigidbody.linearVelocityX) + 1))/scaleFactor;
        transform.localScale = scale;
    }
    LookDirection GetLookDirection() 
    {
        LookDirection watching;
        watching = LookDirection.east;
        if (Input.GetKey(KeyCode.A)) 
        {
            watching = LookDirection.west;
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            watching = LookDirection.south;
        }
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            watching = LookDirection.north;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            watching = LookDirection.southwest;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            watching = LookDirection.northwest;
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
        {
            watching = LookDirection.southeast;
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
        {
            watching = LookDirection.northeast;
        }
        return watching;
    }
    public void TakeDamage(int damage, Collider2D other)
    {
        HP -= damage;
        Vector2 direction = -(other.transform.position - this.transform.position).normalized;
        StartCoroutine(KnockbackCoroutine(direction));
    }
    IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        isKnockback = true;
        Vector2 force = direction * knockbackForce;
        myRigidbody.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
    }
}
