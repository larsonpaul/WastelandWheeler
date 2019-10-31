using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDiffcultyAdjuster //ICreatureStats
{

    public float health;
    public float healthMax = 5;

    public float iframe = 0;
    public float iframeMax = 0;

    public float speed;
    public float baseSpeed = 40;

    public float firerate;
    public float baseFirerate = 0;

    public float damage = 10;
    public float baseDamage = 10;

    private EnemyBar healthBar;

    private DynamicDifficultyAdjuster dda;

    public Spawn_Manager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<EnemyBar>();

        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        dda.Subscribe(this);

        health = healthMax;
        speed = baseSpeed * (1.0f + (0.05f * dda.GetDifficulty()));
        firerate = baseFirerate;
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
        dda.Unsubscribe(this);
        Destroy(gameObject);
        spawnManager.EnemyDefeated();
        // spawn powerup
    }

    public void ChangeDifficulty(int amount)
    {
        speed = baseSpeed * (1.0f + (0.05f * dda.GetDifficulty()));
    }


}
