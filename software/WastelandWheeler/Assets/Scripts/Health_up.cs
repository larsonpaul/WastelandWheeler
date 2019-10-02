using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_up : MonoBehaviour
{
    public float heal = 20f;

    private static Player_stats stats;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            stats.addHealth(heal);
        }
    }
}
