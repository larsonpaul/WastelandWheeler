using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MovementBase
{

    public override Vector2 getDirection()
    {
        Vector2 mypos = gameObject.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector2 ppos = player.transform.position;
            return (ppos - mypos).normalized;
        }
        return new Vector2(0, 0);
    }

}
