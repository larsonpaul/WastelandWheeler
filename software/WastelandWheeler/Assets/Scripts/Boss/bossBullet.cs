
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour, IDiffcultyAdjuster
{
    public float damage;

    private Transform player;
    private Vector2 target;
    Player_stats playerStats;   // used to deal damage
    private Rigidbody2D rb;
    public float bulletLife;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //GameObject effect = Instantiate(hit_effect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        GameObject obj = other.gameObject;

        // cases where bullet is not destroyed
        string[] tags = { "Enemy", "Power_Up", "bullet"};
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

            Debug.Log("bullet push back");
            Vector2 knockback = gameObject.GetComponent<Rigidbody2D>().velocity;
            playerRB.AddForce(knockback * 30);
            return;
        }

        else
        {
            return;
        }

    }


    public void StartDifficulty(int difficulty)
    {
        float difficulty_mod = (1 + 0.1f * difficulty);

    }

    public void ChangeDifficulty(int difficulty)
    {
        damage += difficulty;
        Debug.Log("Bullet Damage: " + damage);
    }

}