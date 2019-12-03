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
    private float dazedLength = 4.0f;
    private float dazedTime;
    private float baseDaze;
    private Player_stats ps;

    private DynamicDifficultyAdjuster dda;

    private Stat_Manager stat_manager;
    private int difficulty;

    private GameObject icon; // icon used to indicate player is slowed down
    private float flickerTimer;
    private float flickerCount = 10;
    private bool destroyed;

    private void Start()
    {
        baseCarDamage = carDamage;
        baseDaze = dazedLength;

        ps = FindObjectOfType<Player_stats>();

        stat_manager = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
        difficulty = stat_manager.GetDifficulty();
        StartDifficulty(difficulty); // will make enemies harder as player progresses through the game
        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        dda.Subscribe(this);

        icon = GameObject.Find("GameUI").transform.Find("DownSpeedIcon").gameObject;
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

        if (thrown && collision && Time.time > flickerTimer)
        {
            destroyCar();
            flickerCount--;
            if (flickerCount == 0)
            {
                destroyed = true;
            }
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
            collision = true;
            Debug.Log("Thrown car hit player");
            if ((ps.healthCurrent - carDamage) > 0)
            {
                dazed = true;
                dazedTime = Time.time + dazedLength;
            }

            ps.RemoveHealth(carDamage);
        }
        else
        {
            Debug.Log("Collsion with other " + col);
            collision = true;
        }

        flickerTimer = Time.time + 2.0f;
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

        if(dazedLength > 1.0f)
        {
            dazedLength = baseDaze + difficulty;
            Debug.Log("DazeTime: " );
        }
    }

    public void slowDown()
    {
        if(Time.time < dazedTime)
        {
            Debug.Log("Dazed " );
            ps.move_speed = 40;
            icon.SetActive(true);
        }
        else
        {
            Debug.Log(" Not Dazed anymore");
            ps.move_speed = 80;
            dazed = false;
            icon.SetActive(false);
        }
    }

    private void destroyCar()
    {
        if (destroyed)
        {
            //gameObject.active = false;
        }
        else if (gameObject.GetComponent<Renderer>().enabled == true)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            flickerCount--;
            flickerTimer = Time.time + .1f;
        }
        else if (gameObject.GetComponent<Renderer>().enabled == false)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            flickerCount--;
            flickerTimer = Time.time + .1f;
        }
    }
}
