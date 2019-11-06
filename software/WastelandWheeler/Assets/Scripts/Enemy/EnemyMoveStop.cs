using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveStop : MonoBehaviour
{
    private Rigidbody2D rbody;
    private EnemyStats stats;

    private Vector2 dir;
    private float dist;

    public float range = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = getDirection();
        dist = getDistance();

        if (dist > range)
        {
            rbody.AddForce(new Vector2(stats.speed * dir.x, stats.speed * dir.y));
        }
            
          
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

    float getDistance()
    {
        Vector2 mypos = gameObject.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector2 ppos = player.transform.position;
            return Vector2.Distance(mypos, ppos);
        }
        return -1.0f;
    }
}
