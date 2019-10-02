using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROF_up : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 10f;
    public float new_rof;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Pickup(collision));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        Shooting ROF = player.GetComponent<Shooting>();

        ROF.fire_rate /= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        ROF.fire_rate *= multiplier;

        Destroy(gameObject);
    }
}
