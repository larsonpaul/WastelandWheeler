using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCar : MonoBehaviour
{
    public float carDamage = 40.0f;
    public bool collision;
    public bool thrown;
    int knockX = 10;
    int knockY = 10;
    Vector2 knockback;
    float rotationTime;
    int rotationMultiplier = 1;

    private void Update()
    {
        if (thrown && Time.time > rotationTime && !collision)
        {
            rotateCar();
        }
    }

    void rotateCar()
    {

        transform.eulerAngles = Vector3.forward * 45 * rotationMultiplier;
        rotationTime = Time.time + .2f;

        if (rotationMultiplier == 4)
        {
            rotationMultiplier = 1;
        }
        else
        {
            rotationMultiplier++;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && thrown || col.gameObject.CompareTag("Shot")
            || col.gameObject.CompareTag("bullet"))
        {
            Debug.Log("Collision with " + col + " nothing should happpen");
        }

        else if (col.gameObject.CompareTag("ThrownCar"))
        {

            Rigidbody2D carRB = gameObject.GetComponent<Rigidbody2D>();
            knockback = carRB.velocity;
            carRB.AddForce(-knockback * 0);
            collision = true;
        }

        else if (col.gameObject.CompareTag("Player") && !collision && thrown)
        {

            col.gameObject.GetComponent<Player_stats>().RemoveHealth(carDamage);
            collision = true;
            Debug.Log("Thrown car hit player");
        }
        else
        {
            Debug.Log("Collsion with other " + col);
            collision = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (thrown)
        {
            collision = true;
        }
    }

}
