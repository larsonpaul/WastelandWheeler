using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script that will, on collision, reduce a player's health by a damage variable and knock them back
 */
public class BossDamage : MonoBehaviour
{
    public float damage = 10f;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;

        // cases where bullet is not destroyed
        string[] tags = { "Enemy", "Power_Up", "bullet", "Magnet" };
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

            Debug.Log("Boss push back");
            return;
        }
    }
}
