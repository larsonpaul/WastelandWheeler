using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROF_up : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 10f;
    public float new_rof;

    private static Player_stats stats;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Pickup(collision));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        stats.rate_of_fire /= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        stats.rate_of_fire *= multiplier;

        Destroy(gameObject);
    }
}
