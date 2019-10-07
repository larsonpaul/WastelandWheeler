using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that controls the player's ROF
 */
public class ROF_up : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 10f;
    private static Player_stats stats;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // On collision, check the player tag and increase the ROF based on a multiplier
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            stats.rate_of_fire /= multiplier;
            Invoke("Disable", duration);
        }
    }

    // Remove players increased ROF after a duration
    void Disable()
    {
        stats.rate_of_fire *= multiplier;
        Destroy(gameObject);
    }
}
