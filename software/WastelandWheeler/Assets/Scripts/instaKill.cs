using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instaKill : MonoBehaviour
{
    // When a collision occurs, check for a player tag and reduce the player health by damage.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().killPlayer();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().killPlayer();
        }
    }
}
