﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{
    public float damage;

    private Transform player;
    private Vector2 target;
    Player_stats playerStats;   // used to deal damage
    private Rigidbody2D rb;
    private int lifetime = 200;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //eventually, bullet dies
        if (lifetime == 0)
        {
            Destroy(gameObject);
        }
        lifetime--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;

        // cases where bullet is not destroyed
        string[] tags = { "Enemy", "Power_Up", "bullet", "boss", "Magnet" };
        for (int i = 0; i < tags.Length; i++)
        {
            if (obj.CompareTag(tags[i])) return;
        }
        // cases where bullet is destroyed
        if (obj.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            Debug.Log("Player hit" + damage + " damage");

            Rigidbody2D playerRB = obj.gameObject.GetComponent<Rigidbody2D>();

            //Debug.Log("bullet push back");
            Vector2 knockback = gameObject.GetComponent<Rigidbody2D>().velocity;
            playerRB.AddForce(knockback * 30);

            Destroy(gameObject);
            return;
        }

        else
        {
            return;
        }
    }
}