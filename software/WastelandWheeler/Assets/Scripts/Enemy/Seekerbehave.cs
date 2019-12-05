using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seekerbehave : MonoBehaviour
{
    private Rigidbody2D rbody;
    private EnemyStats stats;

    public float speed = 250.0f;
    private Vector2 dir;
    private float dist;
    public GameObject bullet;
    public float range = 5.0f;


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
        dist = getDistance();

        if (dist > range)
        {
            rbody.AddForce(new Vector2(stats.speed * dir.x, stats.speed * dir.y));
        }
        else
        {
            int shots = 10;

            float angle = 360f / shots;

            for (float i=0f; i < 360; i += angle)
            {
                Quaternion q = Quaternion.AngleAxis(i, Vector3.up);
                Debug.Log(q.ToString());
                GameObject projectile = (GameObject)Instantiate(bullet, transform.position, q);
                projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.up * speed);
            }

            //GameObject projectile = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.7f* speed, 0.7f * speed));
            //GameObject projectile2 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.7f * speed, 0.7f * speed));
            //GameObject projectile3 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile3.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.7f * speed, -0.7f * speed));
            //GameObject projectile4 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile4.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.7f * speed, -0.7f * speed));
            //GameObject projectile5 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile5.GetComponent<Rigidbody2D>().AddForce(new Vector2(0 * speed, 1 * speed));
            //GameObject projectile6 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile6.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * speed, 0 * speed));
            //GameObject projectile7 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile7.GetComponent<Rigidbody2D>().AddForce(new Vector2(0 * speed, -1 * speed));
            //GameObject projectile8 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            //projectile8.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * speed, 0 * speed));

            stats.OnDeath();
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
