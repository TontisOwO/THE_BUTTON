using UnityEngine;

public class WalkingSoundEffectCuz : MonoBehaviour
{
    private AudioSource audioSource;

    public Movement movement; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) && movement.onGround == true)
        {
            audioSource.Play();
        }
    }
}
