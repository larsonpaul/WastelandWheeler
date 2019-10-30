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

    private GameObject player;
    public LevelManager levelManager;
	private BossFightOne2D fight;

	// Start is called before the first frame update
	void Start()
    {
		fight = FindObjectOfType<BossFightOne2D>();
		levelManager = FindObjectOfType<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            player.GetComponent<Player_stats>().
                RemoveHealth(player.GetComponent<Player_stats>().healthCurrent);
            Debug.Log("Health is 0");
            levelManager.respawnPlayer();
			fight.stopBossFight();
        }
    }
}
