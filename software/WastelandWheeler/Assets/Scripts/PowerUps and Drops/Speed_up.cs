﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that controls the player's move speed
 */
public class Speed_up : MonoBehaviour
{
    public float speed = 100f;
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;
    private static int active = 0;

    private GameObject icon;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        icon = GameObject.Find("GameUI").transform.Find("SpeedIcon").gameObject;
    }

    // On collision, check the player tag and increase the move speed based on a multiplier
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            stats.playPowerup = true;
            StartCoroutine(Power());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private IEnumerator Power()
    {
        if (stats.move_speed == stats.baseSpeed) active = 0;

        active += 1;
        
        if (active == 1)
        {
            stats.move_speed = speed;
            if (stats.isSlowed) stats.move_speed /= 2;
            icon.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        active -= 1;
        if (active == 0)
        {
            stats.move_speed = stats.baseSpeed;
            if (stats.isSlowed) stats.move_speed /= 2;
            icon.SetActive(false);
        }
        Destroy(gameObject);
    }
}