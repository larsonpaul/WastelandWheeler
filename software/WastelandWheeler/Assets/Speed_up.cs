using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_up : MonoBehaviour
{
    public float multiplier = 1.5f;
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(Pickup(collision));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        player_movement move_speed = player.GetComponent<player_movement>();

        move_speed.move_speed *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        move_speed.move_speed /= multiplier;

        Destroy(gameObject);
    }
}
