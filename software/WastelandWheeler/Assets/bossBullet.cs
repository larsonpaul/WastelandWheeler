
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{
    public float damage;

    private Transform player;
    private Vector2 target;
    Player_stats playerStats;   // used to deal damage
    private Rigidbody2D rb;

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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Physics2D.IgnoreLayerCollision(9, 11);

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
        Destroy(gameObject);
    }

}