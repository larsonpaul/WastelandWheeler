﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that will add a health value to the player_stats class on collision with a health object
 */
public class Health_up : MonoBehaviour
{
    public float baseHeal = 0.20f;
    private static Player_stats stats;

    private bool used = false;

    private DynamicDifficultyAdjuster dda;

    // On collision, check for player tag and add health
    private void OnTriggerEnter2D(Collider2D col)
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            stats.playPowerup = true;
            Destroy(gameObject);

            int difficulty = dda.GetDifficulty();
            float heal = baseHeal * (1.0f - (0.01f * difficulty));

            stats.HealPercent(heal);
        }
    }
}
