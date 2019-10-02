using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility_up : MonoBehaviour
{
    public float invince_shield = 999f;
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

            stats.isInvincible = true;

            Invoke("Disable", duration);
        }
    }

    void Disable()
    {
        stats.isInvincible = false;
        Destroy(gameObject);
    }
}
