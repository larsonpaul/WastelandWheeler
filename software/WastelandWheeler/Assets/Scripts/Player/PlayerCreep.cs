using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreep : MonoBehaviour
{
    // made public to test damage values
    public float damage = 5f;
    private int lifetime = 100;

    // checked each frame
    void Update() 
    {
        // check if it has burned out
        if (lifetime == 0)
        {
            Destroy(gameObject);
        }
        lifetime--;
    }

    // collided with something 
    void OnTriggerStay2D(Collider2D col)
    {

        // Check enemy collision and remove its health
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyStats>().RemoveHealth(damage);
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
        }
    }
}
