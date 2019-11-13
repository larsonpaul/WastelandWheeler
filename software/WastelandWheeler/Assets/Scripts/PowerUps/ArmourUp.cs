using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourUp : MonoBehaviour
{
    public float armour = 10f;
    private static Player_stats stats;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // On collision, check for player tag and add armour
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            stats.AddArmour(armour);
        }
    }
}
