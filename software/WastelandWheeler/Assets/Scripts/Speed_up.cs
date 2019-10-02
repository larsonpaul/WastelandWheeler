using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_up : MonoBehaviour
{
    public float multiplier = 1.5f;
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

            stats.move_speed *= multiplier;

            Invoke("Disable", duration);
        }
    }

    void Disable()
    {
        stats.move_speed /= multiplier;
        Destroy(gameObject);
    }
}
