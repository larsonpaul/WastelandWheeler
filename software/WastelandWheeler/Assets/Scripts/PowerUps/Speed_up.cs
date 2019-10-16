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

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // On collision, check the player tag and increase the move speed based on a multiplier
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            stats.move_speed *= multiplier;
            Invoke("Disable", duration);
        }
    }

    // Remove players increased move speed after a duration
    void Disable()
    {
        stats.move_speed /= multiplier;
        Destroy(gameObject);
    }
}
