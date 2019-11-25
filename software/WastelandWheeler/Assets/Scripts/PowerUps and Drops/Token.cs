using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    Vector2 playerDirection;

    int value = 1;

    private bool used = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            col.GetComponent<Player_stats>().playCoin = true;
            col.GetComponent<Player_stats>().totalCoins += value;
            Destroy(gameObject);
        }
    }
}
