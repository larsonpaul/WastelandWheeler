using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    private static Player_stats stats;

    private bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        if (stats.Equals(null))
        {
            return;
        }
        else
        {
            stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            stats.playPowerup = true;

            stats.AddLife();

            Destroy(gameObject);
        }
    }
}
