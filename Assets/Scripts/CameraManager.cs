using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Movement player;
    [SerializeField] float cameraMoveSpeed;
    [SerializeField] float[] levelEdges;
    Vector2 cameraPosition;

    void Update()
    {
        cameraPosition = transform.position;
        cameraPosition = Vector2.Lerp(player.transform.position, cameraPosition, Mathf.Pow(0.5f, cameraMoveSpeed * Time.deltaTime));
        if (cameraPosition.x <= levelEdges[0])
        {
            cameraPosition.x = levelEdges[0];
        }
        if (cameraPosition.y <= levelEdges[1])
        {
            cameraPosition.y = levelEdges[1];
        }
        if (cameraPosition.x >= levelEdges[2])
        {
            cameraPosition.x = levelEdges[2];
        }
        if(cameraPosition.y >= levelEdges[3])
        {
            cameraPosition.y = levelEdges[3];
        }
        transform.position = cameraPosition;
    }
}
