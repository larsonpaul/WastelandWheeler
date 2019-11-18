using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Change to damage reduction for a limited time
public class ArmourUp : MonoBehaviour
{
    public float armour = .25f;
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
