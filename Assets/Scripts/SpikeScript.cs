using System.Collections;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool cooldownActive = false;

    [SerializeField] Movement player;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !cooldownActive)
        {
            //deal damage to the player equal to the damage value
            player.HP -= damage;
            StartCoroutine(DamageCooldown(0.25f));

        }
    }

    IEnumerator DamageCooldown(float duration)
    {
        cooldownActive = true;
        yield return new WaitForSeconds(duration);
        cooldownActive = false;
        yield return null;
    }
}
