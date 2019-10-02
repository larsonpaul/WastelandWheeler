using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_up : MonoBehaviour
{
    public float multiplier = 1.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup(collision);
        }
    }

    void Pickup(Collider2D player)
    {
        Player_stats stats = player.GetComponent<Player_stats>();

        stats.health *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;


        Destroy(gameObject);
    }
}
