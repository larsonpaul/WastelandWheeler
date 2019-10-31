using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MovementBase
{
    public override Vector2 getDirection()
    {
        Vector2 mypos = gameObject.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector2 ppos = player.transform.position;
            return (ppos - mypos).normalized;
        }
        return new Vector2(0, 0);
    }

    public Rigidbody2D rgbody;
    public float damage = 10f;

    // Start is called before the first frame update, find the rbody of the object 
    void Start()
    {
        rgbody = GetComponent<Rigidbody2D>();
    }

    // When a collision occurs, check for a player tag and reduce the player health by damage.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rgbody.velocity *= -2;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
        }
    }

    void Die()
    {
        //Do things like set to respawn, drop crafting parts, give adrenaline to player
        //Right now the ememy is destroyed when the bullet enters its collider (should we move it being destroyed into this script?)
    }

}
