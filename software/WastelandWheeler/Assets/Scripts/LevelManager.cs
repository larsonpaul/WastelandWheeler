using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Used in conjuntion with DamagePlayer script. Create an Object name LevelManager and CheckPoint.
 * In the inspector, move the CheckPoint Object into the currentCheckPoint variable spot.  See DamagePlayer
 * for more info
 */

public class LevelManager : MonoBehaviour
{

    public GameObject currentCheckPoint;
    private PlayerMovement_Side player;
    private player_movement player_move;
    public bool isTopDownMove = true;
    private Player_stats player_Stats;

    // Start is called before the first frame update
    void Start()
    {
        if (!isTopDownMove)
        {
            player = FindObjectOfType<PlayerMovement_Side>();
        }
        else
        {
            player_move = FindObjectOfType<player_movement>();
        }

        player_Stats = FindObjectOfType<Player_stats>();
    }

    public void respawnPlayer()
    {
        Debug.Log("Player respawned here");
        player.transform.position = currentCheckPoint.transform.position;
        player_Stats.refillHealth();
    }
}
