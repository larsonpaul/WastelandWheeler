using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCar : MonoBehaviour
{
    public float carDamage = 40.0f;
    public bool collision = true;
    public bool thrown = false;
    int knockX = 10;
    int knockY = 10;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && thrown)
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(carDamage);
            thrown = false;
        }

        if (col.gameObject.CompareTag("ThrownCar") && thrown)
        {

            //calculate knock back
            if (col.transform.position.x < transform.position.x)
            {
                knockX = -knockX;
            }
            if (col.transform.position.y < transform.position.y)
            {
                knockY = -knockY;
            }

            //knockback player
            Rigidbody2D carRB = col.gameObject.GetComponent<Rigidbody2D>();
            carRB.bodyType = RigidbodyType2D.Static;
            carRB.velocity = new Vector2(knockX, knockY);
        }


    }
}
