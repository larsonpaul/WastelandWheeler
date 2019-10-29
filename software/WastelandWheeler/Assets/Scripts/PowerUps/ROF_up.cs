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

    private bool used = false;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // On collision, check the player tag and increase the ROF based on a multiplier
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;

            StartCoroutine(stats.PowerROF(multiplier, duration));

            Destroy(gameObject);
        }
    }
}
