using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDiffcultyAdjuster, ICreatureStats
{
    public float health;
    public float healthMax = 5;

    public float iframe = 0;
    public float iframeMax = 0;

    public float speed;
    public float baseSpeed = 40;

    public float firerate;
    public float baseFirerate = 0;

    public float damage = 10f;
    public float baseDamage = 10;

    public float adrenalineYield = 5.0f;

    private EnemyBar healthBar;

    private GameManager gameManager;

    private Stat_Manager stat_manager;
    private int difficulty;

    public bool bossDeath;
    private float deathDelay;

    // Start is called before the first frame update
    void Start()
    {
        stat_manager = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
        difficulty = stat_manager.GetDifficulty();
        StartDifficulty(difficulty); // will make enemies harder as player progresses through the game

        health = healthMax;
        speed = baseSpeed;
        firerate = baseFirerate;
        damage = baseDamage;

        healthBar = GetComponent<EnemyBar>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.CreateEnemy(this);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (iframe > 0)
        {
            iframe -= 1;
        }

        if (bossDeath)
        {
            OnDeath();
        }

    }


    public float GetHealth()
    {
        return health;
    }


    //manipulating enemy health
    public void AddHealth(float num)
    {
        if (num <= 0)
        {
            if (num < 0)
            {
                string error = gameObject.name + ".AddHealth() given negative float " + num;
                Debug.LogError(error);
            }
            return;
        }
        else if (num > 0)
        {
            health = Mathf.Min(health + num, healthMax);
        }
        healthBar.SetScale(health / healthMax);
    }

    public void RemoveHealth(float num)
    {
        if (num <= 0 || iframe > 0)
        {
            if (num < 0)
            {
                string error = gameObject.name + ".RemoveHealth() given negative float " + num;
                Debug.LogError(error);
            }
            return;
        }
        else
        {
            health -= num;
            iframe = iframeMax;
            if (health <= 0)
            {
                OnDeath();
                return;
            }
            healthBar.SetScale(health / healthMax);
        }
    }

    // getters and setters
    public float GetSpeed()
    {
        return speed;
    }

    public void ModifySpeed(float mod)
    {
        speed *= mod;
    }

    public float GetFirerate()
    {
        return firerate;
    }

    public void ModifyFirerate(float mod)
    {
        firerate *= mod;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void OnDeath()
    {
        if (gameObject.tag == "boss")
        {

            if (Time.deltaTime > deathDelay && !bossDeath)
            {
                // don't kill the boss immediatly
                Debug.Log("Boss on death");
                healthBar.SetScale(0);
                Transform bar = transform.Find("HealthBar");
                bar.gameObject.SetActive(false);
                deathDelay = Time.time + 1.0f;
                bossDeath = true;
            }
            else if (Time.time > deathDelay)
            {

                Debug.Log("death drop");

                // kill the boss after delay
                gameObject.GetComponent<Renderer>().enabled = false;
                ParticleSystem deathDrop = FindObjectOfType<ParticleSystem>();
                Instantiate(deathDrop, gameObject.transform.position, gameObject.transform.rotation);
                gameManager.KillBoss(this);
                bossDeath = false;
            }

        }
        else if (gameObject.tag == "Enemy")
        {
            Debug.Log("Normal Emeny killed");
            gameManager.KillEnemy(this);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(gameObject.tag + " was killed");
        }

    }

    // method that will make the enemies harder based on the persistent difficulty increase through each level
    public void StartDifficulty (int difficulty) {
        float difficulty_mod = (1 + 0.1f * difficulty);
        healthMax = healthMax * difficulty_mod;
        baseDamage = baseDamage * difficulty_mod;
    }

    public void ChangeDifficulty(int difficulty)
    {
        speed = baseSpeed * (1.0f + (0.05f * difficulty));
    }


}
