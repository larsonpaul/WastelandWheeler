using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    private static Player_stats stats;

    private bool used = false;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();

        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            stats.playPowerup = true;

            stats.AddLife();

            Destroy(gameObject);
        }
    }
}
