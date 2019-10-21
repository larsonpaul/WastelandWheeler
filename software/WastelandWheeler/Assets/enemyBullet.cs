using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed;
    public bool just_along_x;
    public bool just_along_y;
    public float damage;

    private Transform player;
    private Vector2 target;
    Player_stats playerStats;
     

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (just_along_x)
        {
            target = new Vector2(player.position.x, transform.position.y);
        } 
        else if (just_along_y)
        {
            target = new Vector2(transform.position.x, player.position.y);
        }
        else
        {
            target = new Vector2(player.position.x, player.position.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            
            destroyEnemyBullet();
        }


    }

    void destroyEnemyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      
        
        if (other.CompareTag("Enemy")){
            Physics2D.IgnoreLayerCollision(9, 11);
        }

        else if (other.tag == ("Player")){
            other.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            Debug.Log("Player hit" + damage + " damage");
        }

        else
        {
            //Debug.Log("Collision")
            destroyEnemyBullet();
        }
       
    }

}
