using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    Rigidbody2D rbody;

    public float damage = -10f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rbody.velocity *= -2;
            col.gameObject.GetComponent<Player_stats>().addHealth(damage);
        }
    }
}
