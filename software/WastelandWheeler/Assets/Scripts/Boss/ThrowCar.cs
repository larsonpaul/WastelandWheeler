using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCar : MonoBehaviour, IDiffcultyAdjuster
{
    public float carDamage = 40.0f;
    private float baseCarDamage;

    public bool collision;  // see whether the car has hit the boundry
    public bool thrown; // used to initiate damage and rotation
    Vector2 knockback;
    float rotationTime; 
    int rotationMultiplier = 1; // used to determine which way to rotate the car

    private bool dazed;
    public  float dazedTime = 8.0f;
    private float baseDaze;
    private Player_stats ps;

    private DynamicDifficultyAdjuster dda;

    private Stat_Manager stat_manager;
    private int difficulty;

    private void Start()
    {

        baseCarDamage = carDamage;
        baseDaze = dazedTime;

        ps = FindObjectOfType<Player_stats>();

        stat_manager = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
        difficulty = stat_manager.GetDifficulty();
        StartDifficulty(difficulty); // will make enemies harder as player progresses through the game
        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        dda.Subscribe(this);
    }

    private void Update()
    {
        //rotate thrown car
        if (thrown && Time.time > rotationTime && !collision)
        {
            rotateCar();
        }

        // slow down player if hit by car
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
            if (ps.healthCurrent <= 0)
        { 
            dazed = true;
            dazedTime += Time.time;
        }
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
        carDamage =  (baseCarDamage + (difficulty * 5.0f));
        Debug.Log("car damge: " + carDamage);

        if(dazedTime > 1.0f)
        {
            dazedTime = baseDaze + difficulty;
            Debug.Log("DazeTime: " + dazedTime);
        }

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
