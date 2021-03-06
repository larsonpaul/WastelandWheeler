﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBehave : MonoBehaviour
{
    public float damage = 20f;
    private int lifetime = 100;
  
    void Update() // called each frame
    {
        // check if it has burned out
        if (lifetime == 0)
        {
            Destroy(gameObject);
        }
        lifetime--;
    }

    //collided with something 
    void OnTriggerStay2D(Collider2D col)
    {

        //check if what is has collided wwith should destroy it or not
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
        }
    }
}
