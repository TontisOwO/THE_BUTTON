using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject goHere;
    [SerializeField] bool puzzleDone = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void DoorOpen()
    {
        if ( puzzleDone == false )
        {
            door.transform.position = goHere.transform.position;
            puzzleDone = true;
        }
    }
}
