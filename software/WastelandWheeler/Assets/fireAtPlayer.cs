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
            Instantiate(bullet, transform.position, Quaternion.identity);
            rate_of_fire = shotStartTime;

        }

        else
        {
            rate_of_fire -= Time.deltaTime;
        }
    }
}
