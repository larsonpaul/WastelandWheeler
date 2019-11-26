using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player_stats>().move_speed *= .5f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Player_stats>().move_speed = collision.GetComponent<Player_stats>().baseSpeed;
    }
}
