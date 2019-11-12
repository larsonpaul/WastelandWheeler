using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireAtPlayer : MonoBehaviour
{

    //counter used to enemy rate of fire
    public float shotStartTime;
    private float rate_of_fire;

    //enemy sightlines
    public float playerWithinRangeX;
    public float playerWithinRangeY;

    public GameObject bullet;
    private Transform player;

    private Vector2 target;
    public float speed;
    public bool just_along_x;  // only shoot sideways
    public bool just_along_y;  // only shoot up and down
    public bool heatSeeker;    // bullet floows player
    public bool burstShot;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rate_of_fire = shotStartTime;
        if (heatSeeker)
        {
            target = new Vector2(player.position.x, player.position.y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Show enemy Sight lines
        Debug.DrawLine
            (new Vector3(transform.position.x + playerWithinRangeX, transform.position.y, transform.position.z),
             new Vector3(transform.position.x - playerWithinRangeX, transform.position.y, transform.position.z));

        Debug.DrawLine
            (new Vector3(transform.position.x, transform.position.y + playerWithinRangeY, transform.position.z),
             new Vector3(transform.position.x, transform.position.y - playerWithinRangeY, transform.position.z));


        // If player within range and 
        if (Vector2.Distance(transform.position, player.position) < playerWithinRangeX && rate_of_fire <= 0 ||
            Vector2.Distance(transform.position, player.position) < playerWithinRangeX && rate_of_fire <= 0)
        {
            if (heatSeeker)
            {
                Debug.Log("heat Seek");
                heatSeek();
                rate_of_fire = shotStartTime;
            }
            else
            {
                fireProjectile();
                rate_of_fire = shotStartTime;
            }
        }

        else
        {
            rate_of_fire -= Time.deltaTime;
        }
    }

    // Create enemy bullet
    void fireProjectile()
    {
        target = player.transform.position - transform.position;
        GameObject projectile = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);

        if (just_along_x) // shoot sideways
        {
            Debug.Log("Shot fired just x");
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x * speed, transform.position.y));
        }
        else if (just_along_y) // shoot up and down
        {
            Debug.Log("Shot fired just y");
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x, target.y * speed));
            
        }

        else if (burstShot) // shot three spreading bullets
        {
            Debug.Log("Shot fired burst");
            Vector2 perpendicular = Vector2.Perpendicular(target);
            Vector2 angle1 = (0.4f * perpendicular) + target;
            Vector2 angle2 = -(0.4f * perpendicular) + target;
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x * speed, target.y * speed));
            GameObject projectile2 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            projectile2.GetComponent<Rigidbody2D>().AddForce(new Vector2(angle2.x * speed, angle2.y *speed));
            GameObject projectile3 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            projectile3.GetComponent<Rigidbody2D>().AddForce(new Vector2(angle1.x * speed, angle1.y *speed));
        }
        else
        {
            Debug.Log("Shot fired diagonal"); // shoot diagnolly
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x * speed, target.y * speed));

        }
    }

    void heatSeek() // bullet follws the enemy
    {
        GameObject projectile = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        projectile.transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
}
