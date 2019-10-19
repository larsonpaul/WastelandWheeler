using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireAtPlayer : MonoBehaviour
{

    public float shotStartTime;
    private float rate_of_fire;
    public float playerWithinRange;

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
        if (Vector2.Distance(transform.position, player.position) < playerWithinRange)
        {

        }

        if (rate_of_fire <= 0)
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
