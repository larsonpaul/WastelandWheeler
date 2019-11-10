using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script that will, on collision, reduce a player's health by a damage variable and knock them back
 */
public class BossDamage : MonoBehaviour
{
	public Rigidbody2D rbody;
	public float damage = 10f;
    int knockX = 10;
    int knockY = 10;
    public GameObject player;
	//public EnemyStats stats;

	// Start is called before the first frame update, find the rbody of the object 
	void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		//stats = GetComponent<EnemyStats>();

		//damage = stats.GetDamage();
	}

    private void Update()
    {
        //calculate knock back
        if (player.transform.position.x < transform.position.x)
        {
            knockX = -knockX;
        }
        if (player.transform.position.y < transform.position.y)
        {
            knockY = -knockY;
        }
    }


    void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
            // remove player health
			col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);

            //knockback player
            Rigidbody2D carRB = col.gameObject.GetComponent<Rigidbody2D>();
            carRB.velocity = new Vector2(knockX,knockY);
            PolygonCollider2D bc = gameObject.GetComponent<PolygonCollider2D>();
            //bc.isTrigger = true;
        }
	}
}
