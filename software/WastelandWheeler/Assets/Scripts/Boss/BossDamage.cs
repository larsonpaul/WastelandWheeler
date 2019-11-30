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


    private void OnTriggerEnter2D(Collider2D other)
    {

        //GameObject effect = Instantiate(hit_effect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        GameObject obj = other.gameObject;

        // cases where bullet is not destroyed
        string[] tags = { "Enemy", "Power_Up", "bullet" };
        for (int i = 0; i < tags.Length; i++)
        {
            if (obj.CompareTag(tags[i])) return;
        }
        // cases where bullet is destroyed
        if (obj.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            Debug.Log("Player hit" + damage + " damage");

            Rigidbody2D playerRB = obj.gameObject.GetComponent<Rigidbody2D>();

            Debug.Log("Boss push back");
            Vector2 knockback = gameObject.GetComponent<Rigidbody2D>().velocity;
            playerRB.AddForce(knockback * 60);
            return;
        }

        else
        {
            return;
        }

    }
}
