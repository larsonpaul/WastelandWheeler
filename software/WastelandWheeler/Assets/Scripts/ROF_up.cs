using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROF_up : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 10f;

    private static Player_stats stats;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            stats.rate_of_fire /= multiplier;

            Invoke("Disable", duration);
        }
    }

    void Disable()
    {
        stats.rate_of_fire *= multiplier;
        Destroy(gameObject);
    }
}
