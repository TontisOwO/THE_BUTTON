using UnityEngine;

public class HydraulicDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //LoadScene("loseMenu")
            Debug.Log("Touched Player");
        }
    }
}
