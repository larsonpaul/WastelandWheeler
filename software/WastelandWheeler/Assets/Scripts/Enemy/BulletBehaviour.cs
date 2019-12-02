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
    void OnTriggerStay2D(Collider2D col)
    {
        // check if it should pass through collider
        if (col.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),col);
        }
        else if (col.CompareTag("Shot"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),col);
        }
        else if (col.CompareTag("Power_Up"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),col);
        }
        else if (col.CompareTag("Magnet"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
