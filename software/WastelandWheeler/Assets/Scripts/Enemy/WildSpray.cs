using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildSpray : MonoBehaviour
{
    //counter used to enemy rate of fire
    private float shotStartTime = 0.2f;
    private float rate_of_fire;

    //enemy sightlines
    public float playerWithinRangeX;
    public float playerWithinRangeY;

    public GameObject bullet;
    private Transform player;

    private Vector2 target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rate_of_fire = shotStartTime;

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
            
            
             fireProjectile();
             rate_of_fire = shotStartTime;
        }

        else
        {
            rate_of_fire -= Time.deltaTime;
        }
    }

    // Create enemy bullet
    void fireProjectile()
    {
        //fire at a random trajectory in a cone
        target = player.transform.position - transform.position;
        float distance = target.magnitude;
        Vector2 direction = target / distance;
        GameObject projectile = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        float randomang = Random.Range(-0.7f, 0.7f);
        Vector2 perpendicular = Vector2.Perpendicular(direction);
        Vector2 thisangle = (randomang * perpendicular) + direction;
        //Debug.Log("Shot fired diagonal"); // shoot diagonally
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(thisangle.x * speed, thisangle.y * speed));

    }
}
