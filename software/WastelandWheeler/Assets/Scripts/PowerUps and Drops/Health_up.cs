﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that will add a health value to the player_stats class on collision with a health object
 */
public class Health_up : MonoBehaviour
{
    public float heal = 20f;
    private static Player_stats stats;

    private bool used = false;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // On collision, check for player tag and add health
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            stats.playPowerup = true;
            Destroy(gameObject);

            stats.AddHealth(heal);
        }
    }
}
