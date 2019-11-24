using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCar : MonoBehaviour, IDiffcultyAdjuster
{
    public float carDamage = 40.0f;
    public bool collision;
    public bool thrown;
    int knockX = 10;
    int knockY = 10;
    Vector2 knockback;
    float rotationTime;
    int rotationMultiplier = 1;
    bool dazed;
    private float dazedTime = 4.0f;
    Player_stats ps;

    private void Start()
    {

        ps = FindObjectOfType<Player_stats>();
    }

    private void Update()
    {
        if (thrown && Time.time > rotationTime && !collision)
        {
            rotateCar();
        }

        if (dazed)
        {
            slowDown();
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
            || col.gameObject.CompareTag("bullet") || col.gameObject.CompareTag("Magnet"))
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
            ps.RemoveHealth(carDamage);
            dazed = true;
            dazedTime += Time.time;
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
        string[] tags = { "Shot", "Power_Up", "Magnet", "bullet" };
        for (int i = 0; i < tags.Length; i++)
        {
            if (col.gameObject.CompareTag(tags[i]))
            {
                Debug.Log("Thrown car collided with " + tag);
            }

            else if (thrown)
            {
                collision = true;
            }
        }
    }

    public void StartDifficulty(int difficulty)
    {
        float difficulty_mod = (1 + 0.1f * difficulty);

    }

    public void ChangeDifficulty(int difficulty)
    {
        carDamage += (difficulty * 2.0f);
        Debug.Log("car damge: " + carDamage);

    }

    public void slowDown()
    {
        if(Time.time < dazedTime)
        {
            Debug.Log("Dazed");
            ps.move_speed = 40;
        }
        else
        {
            Debug.Log(" Not Dazed anymore");
            ps.move_speed = 80;
            dazed = false;
        }
        
    }

}
