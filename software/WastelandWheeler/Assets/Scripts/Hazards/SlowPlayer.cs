using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour
{
    private static int sem = 0;

    private Player_stats stats;

    void Start()
    {
        stats = GameObject.Find("Player").GetComponent<Player_stats>();

        sem = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (sem++ == 0)
            {
                stats.move_speed *= .5f;
                stats.isSlowed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (--sem == 0)
            {
                stats.move_speed *= 2f;
                stats.isSlowed = false;
            }
        }
    }
}
