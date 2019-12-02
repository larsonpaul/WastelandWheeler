using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour
{
    private static int sem = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (sem++ == 0)
            {
                col.GetComponent<Player_stats>().move_speed *= .5f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (--sem == 0)
            {
                col.GetComponent<Player_stats>().move_speed *= 2f;
            }
        }
    }
}
