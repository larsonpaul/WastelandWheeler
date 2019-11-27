using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("bullet") || collision.CompareTag("Shot") || collision.CompareTag("Magnet") || collision.CompareTag("Power_Up"))
        {
            return;
        }
        else
        {
            collision.GetComponent<Player_stats>().move_speed *= .5f;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("bullet") || collision.CompareTag("Shot") || collision.CompareTag("Magnet") || collision.CompareTag("Power_Up"))
        {
            return;
        }
        else
        {
            collision.GetComponent<Player_stats>().move_speed *= 2f;
        }
    }
}
