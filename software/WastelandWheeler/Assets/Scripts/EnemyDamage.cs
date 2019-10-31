using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script that will, on collision, reduce a player's health by a damage variable
 */
public class EnemyDamage : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float damage = 10f;

    public EnemyStats stats;

    // Start is called before the first frame update, find the rbody of the object 
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();

        damage = stats.GetDamage();
    }

    // When a collision occurs, check for a player tag and reduce the player health by damage.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rbody.velocity *= -2;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
        }
    }
}
