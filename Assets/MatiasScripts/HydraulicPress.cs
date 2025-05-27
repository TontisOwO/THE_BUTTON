using UnityEngine;

public class HydraulicPress : MonoBehaviour
{
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
}
