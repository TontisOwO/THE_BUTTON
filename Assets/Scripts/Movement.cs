using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Vector2 position;

    void Awake()
    {

    }
    void Update()
    {
        position = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            position.x += movementSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        transform.position = position;
    }
}
