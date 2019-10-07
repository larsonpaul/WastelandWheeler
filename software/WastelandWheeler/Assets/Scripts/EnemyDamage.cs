﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script that will, on collision, reduce a player's health by a damage variable
 */
public class EnemyDamage : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float damage = -10f;

    // Start is called before the first frame update, find the rbody of the object 
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // When a collision occurs, check for a player tag and reduce the player health by damage.
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        rbody.velocity *= -2;
    //        col.gameObject.GetComponent<Player_stats>().addHealth(damage);
    //    }
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rbody.velocity *= -2;
            col.gameObject.GetComponent<Player_stats>().addHealth(damage);
        }
    }
}
