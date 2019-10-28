using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Bullet class that will destroy a bullet object when it collides with an enemy or a surface
 */
public class Bullet : MonoBehaviour
{
    //public GameObject hit_effect;

    // on collision destroy the bullet object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject effect = Instantiate(hit_effect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        GameObject obj = collision.gameObject;

        // cases where bullet is not destroyed
        string[] tags = { "Player", "Shot", "Power_Up" };
        for (int i = 0; i < tags.Length; i++)
        {
            if (obj.CompareTag(tags[i])) return;
        }
        // cases where bullet is destroyed
        Destroy(gameObject);
        if (obj.CompareTag("Enemy"))
        {
            obj.BroadcastMessage("Die");
        }
    }
}
