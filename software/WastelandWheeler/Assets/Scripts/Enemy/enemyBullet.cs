using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyBullet : MonoBehaviour
{
    public float speed;
    public bool just_along_x;  // only shoot sideways
    public bool just_along_y;  // only shoot up and down
    public bool heatSeeker;    // bullet floows player
    public float damage;
    public float bulletSeconds = 2.0f; // the time the bullet lasts before being destroyed 

    private Transform player;
    private Vector2 target;
    Player_stats playerStats;   // used to deal damage
    private Rigidbody2D rb;
    public float seconds = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        destroyBulletAfter();
    }

    void destroyEnemyBullet()
    {
        Destroy(gameObject);
    }

    // collided with something
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if you should pas through or damage
        if (other.CompareTag("Enemy"))
        {
            Physics2D.IgnoreLayerCollision(9, 11);
        }
        if (other.CompareTag("Shot"))
        {
            Physics2D.IgnoreLayerCollision(0, 11);
        }

        //damage player on contact and destory object
        else if (other.tag == ("Player"))
        {
            other.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            Debug.Log("Player hit" + damage + " damage");
            destroyEnemyBullet();
        }

        else
        {
            destroyEnemyBullet();
        }

    }

    // Delayed bullet destroy
    void destroyBulletAfter()
    {
        Destroy(gameObject, bulletSeconds);
    }

}
