﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that will make the player invincible for a limited duration
 */
public class Invincibility_up : MonoBehaviour
{
    public float invince_shield = 999f;
    public float duration = 10f;
    private static Player_stats stats;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // Upon collision, check for player tag and set player invinciblity
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            stats.isInvincible = true;

            Invoke("Disable", duration);
        }
    }

    // remove player invincibility after a given duration
    void Disable()
    {
        stats.isInvincible = false;
        Destroy(gameObject);
    }
}
