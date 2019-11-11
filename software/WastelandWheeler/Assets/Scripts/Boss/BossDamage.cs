using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script that will, on collision, reduce a player's health by a damage variable and knock them back
 */
public class BossDamage : MonoBehaviour
{
	public float damage = 10f;
    int knockX = 10;
    int knockY = 10;
    public GameObject player;
    //public EnemyStats stats;
    Vector2 bulletDirection;

    // Start is called before the first frame update, find the rbody of the object 
    void Start()
	{
        //rbody = GetComponent<Rigidbody2D>();
        //stats = GetComponent<EnemyStats>()
		//damage = stats.GetDamage();
	}

    private void Update()
    {

    }


    void OnTriggerStay2D(Collider2D col)
	{

        //calculate knock back


        if (col.gameObject.CompareTag("Player"))
		{
            bulletDirection = this.gameObject.transform.forward;
            // remove player health
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            //knockback player

            Rigidbody2D playerRB = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 knockback;

            if (gameObject.tag == "boss")
            {
                Debug.Log("Bounce off boss");

                knockback = playerRB.velocity;
                playerRB.AddForce(knockback * 20);

            }

            else
            {
                Debug.Log("bullet push back");
                knockback = gameObject.GetComponent<Rigidbody2D>().velocity;
                playerRB.AddForce(knockback * 20);
            }
            
            PolygonCollider2D bc = gameObject.GetComponent<PolygonCollider2D>();
            //bc.isTrigger = true;
        }
	}
}
