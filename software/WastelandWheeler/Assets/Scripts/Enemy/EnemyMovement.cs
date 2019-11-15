using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rbody;
    private EnemyStats stats;
    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        dir = getDirection();

        rbody.AddForce(new Vector2(stats.speed * dir.x, stats.speed * dir.y));
    }

    Vector2 getDirection()
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

}
