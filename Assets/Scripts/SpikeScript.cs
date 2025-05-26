using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] int damage = 1;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //deal damage to the player equal to the damage value
        }
    }
}
