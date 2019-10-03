using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * To use: attach this script to an emeny or hazard.
 * Make sure to create a CheckPoint Oject and place it in a spot that you want the
 * player to respawn.  On Collision 2D, make sure to check the box for Is Trigger. ** Rigidbody 2D, Body type
 * may have to be changed to Kinematic**
 * */

public class DamagePlayer : MonoBehaviour
{

    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            levelManager.respawnPlayer();
        }
    }
}
