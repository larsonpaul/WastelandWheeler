using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Stat_Manager stat_manager;
    private int difficulty;
    private int dynamicDifficulty;
    public float damage = 10f;
    private int lifetime = 200;


    void Start()
    {
        stat_manager = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
        difficulty = stat_manager.GetDifficulty();
        dynamicDifficulty = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>().GetDifficulty();
        damage = damage * (1 + 0.15f * difficulty);
    }



    void Update()
    {
        //eventually, bullet dies
        if (lifetime == 0)
        {
            Destroy(gameObject);
        }
        lifetime--;
    }

    //collided with something
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            Destroy(gameObject);
        }

        // check if it should pass through collider
        if (!col.isTrigger && !col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
