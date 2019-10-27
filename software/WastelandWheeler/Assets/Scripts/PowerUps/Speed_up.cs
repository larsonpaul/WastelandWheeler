using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that controls the player's move speed
 */
public class Speed_up : MonoBehaviour
{
    public float multiplier = 1.5f;
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // On collision, check the player tag and increase the move speed based on a multiplier
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;

            stats.PowerSpeed(multiplier, duration);

            Destroy(gameObject);
        }
    }
}
