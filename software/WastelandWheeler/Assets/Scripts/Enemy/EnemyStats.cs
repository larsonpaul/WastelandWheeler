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

    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        speed = baseSpeed;
        firerate = baseFirerate;

        healthBar = GetComponent<EnemyBar>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.CreateEnemy(this);

        stat_manager = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
        difficulty = stat_manager.GetDifficulty();
        StartDifficulty(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        if (iframe > 0)
        {
            iframe -= 1;
        }
    }


    public float GetHealth()
    {
        return health;
    }

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
        gameManager.KillEnemy(this);
        Destroy(gameObject);
    }

    public void StartDifficulty (int difficulty) {


    }

    public void ChangeDifficulty(int difficulty)
    {
        speed = baseSpeed * (1.0f + (0.05f * difficulty));
    }


}
