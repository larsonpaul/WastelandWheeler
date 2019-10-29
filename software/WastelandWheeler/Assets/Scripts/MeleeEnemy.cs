using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public EnemyManager enemyManager;

    void Die()
    {
        //Do things like, drop crafting parts, give adrenaline to player
        //timeSlow.EnemyAdrenaline();

        // Used to tell the spawner that an enemy died
        enemyManager.EnemyDefeated();

        // Takes the destroy call from the bullet into the enemy
        Destroy(gameObject);

    }

}
