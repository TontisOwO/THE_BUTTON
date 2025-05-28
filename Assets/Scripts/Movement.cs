using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] float stopSpeed;
    [SerializeField] public float movementSpeedCap = 4;
    [SerializeField] float jumpForce;
    [SerializeField] float scaleFactor = 8;
    [SerializeField] float dashStrength = 200;
    [SerializeField] float dashStopSpeed;
    [SerializeField] float dashEnd;
    [SerializeField] Vector2 LookDirection;
    [SerializeField] bool lookingRight = true;
    public int numberOfDashes;
    public int maxDashes;
    
    public int HP;
    
    Rigidbody2D myRigidbody;
    bool jumpStart;
    public bool dashing;
    float jumpTime;
    float dashTime;
    Vector2 scale;
    public bool onGround;

    bool isKnockback;
    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackDuration;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (onGround && numberOfDashes == 0)
        {
            numberOfDashes = maxDashes;
        }

        scale = transform.localScale;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && myRigidbody.linearVelocityX >= -movementSpeedCap)
        {
            if (onGround)
            {
                lookingRight = false;
            }

            myRigidbody.linearVelocityX -= movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && myRigidbody.linearVelocityX <= movementSpeedCap)
        {
            if (onGround)
            {
                lookingRight = true;
            }

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

        LookDirection = GetLookDirection(lookingRight);
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Q)) && numberOfDashes > 0)
        {
            dashing = true;
            onGround = false;
            numberOfDashes -= 1;
            switch (LookDirection)
            {
                case Vector2 v when v.Equals(Vector2.right):
                    if (myRigidbody.linearVelocityX < 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }

                    myRigidbody.linearVelocityX += dashStrength;
                    break;

                case Vector2 v when v.Equals(Vector2.left):
                    if (myRigidbody.linearVelocityX > 0)
                    {
                        myRigidbody.linearVelocityX = 0;
                    }

                    myRigidbody.linearVelocityX -= dashStrength;
                    break;

                case Vector2 v when v.Equals(Vector2.up):
                    if (myRigidbody.linearVelocityY < 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }

                    myRigidbody.linearVelocityY += dashStrength;
                    break;

                case Vector2 v when v.Equals(Vector2.down):
                    if (myRigidbody.linearVelocityY > 0)
                    {
                        myRigidbody.linearVelocityY = 0;
                    }

                    myRigidbody.linearVelocityY -= dashStrength;
                    break;

                case Vector2 v when v.Equals(new Vector2(1,-1)):
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

                case Vector2 v when v.Equals(Vector2.one):
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

                case Vector2 v when v.Equals(new Vector2(-1,-1)):
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

                case Vector2 v when v.Equals(new Vector2(-1, 1)):

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
    Vector2 GetLookDirection(bool right) 
    {
        Vector2 lookDirection = Vector2.zero;
        if (right)
        {
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            {
                lookDirection.x += 1;
            }
        }
        else
        {
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            {
                lookDirection.x -= 1;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            lookDirection.x -= 1;
            if (!onGround)
            {
                lookDirection.x -= 1;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            lookDirection.x += 1;
            if (!onGround)
            {
                lookDirection.x += 1;
            }
        }
        if (Input.GetKey(KeyCode.W)) 
        { 
            lookDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        { 
            lookDirection.y -= 1;
        }
        if (lookDirection.x > 1)
        {
            lookDirection.x = 1;
        }
        if (lookDirection.x < -1)
        {
            lookDirection.x = -1;
        }
        return lookDirection;
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