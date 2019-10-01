using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility_up : MonoBehaviour
{
    public float invince_shield = 999f;
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Pickup(collision));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        Player_stats stats = player.GetComponent<Player_stats>();

        stats.health += invince_shield;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        stats.health -= invince_shield;

        Destroy(gameObject);
    }
}
