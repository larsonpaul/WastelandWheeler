using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCar : MonoBehaviour
{
    public float carDamage = 40.0f;
    public bool collision;
    public bool thrown;
    public int playerContact = 0;
    int knockX = 10;
    int knockY = 10;
    Vector2 bulletDirection;
    Vector2 knockback;


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && thrown && this.playerContact <1)
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(carDamage);
            playerContact += 1;

        }

        if (col.gameObject.CompareTag("ThrownCar"))
        {

            //knockback player

            Rigidbody2D carRB = gameObject.GetComponent<Rigidbody2D>();
            knockback = carRB.velocity;
            carRB.AddForce(-knockback * 0);
            playerContact += 1;
        }

        else
        {
            Debug.Log("collsion OTHER");
            playerContact += 1;
        }

    }
}
