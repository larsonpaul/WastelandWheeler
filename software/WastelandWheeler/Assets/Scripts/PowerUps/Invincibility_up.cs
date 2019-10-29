using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that will make the player invincible for a limited duration
 */
public class Invincibility_up : MonoBehaviour
{
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // Upon collision, check for player tag and set player invinciblity
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;

            StartCoroutine(stats.PowerInvincible(duration));

            Destroy(gameObject);
        }
    }
}
