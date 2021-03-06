﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class that contains varaibles that control player attributes and has getters for those variables
 */
public class Player_stats : MonoBehaviour, IDiffcultyAdjuster
{
    private Stat_Manager stats;

    public float healthMax = 100f;
    public float healthCurrent = 100f;

    public float baseSpeed = 75f;
    public float move_speed = 75f;

    public float baseROF = 0.2f;
    public float rate_of_fire = 0.2f;

    public float bullet_size = 1f;

    public bool playCoin = false;
    public bool playPowerup = false;

    public bool onFire = false;
    public bool isInvincible = false;
    public bool isThorny = false;
    public bool tripleShot = false;

    public bool isSlowed = false;

    public float adrenalineMax = 100f;
    public float adrenalineCurrent = 100f;

    public float iFrameMax = 20f;
    public float iFrameCur = 0f;

    public float baseDamage = 5f;
    public float damage = 5f;

    public float totalCoins = 0f;

    public float armourMax = 100f;
    public float armourCurrent = 0f;

    public AudioClip powerup;
    public AudioClip coin;
    public AudioClip hit;
    public AudioClip playerDie;
    private AudioSource audio;
    public GameObject damageCanvas;

    public SpawnFire fireTrail;

    [SerializeField]
    private GameManager game;

    [SerializeField]
    private int player_lives;

    private LifeUI lifeUI;

    private DynamicDifficultyAdjuster dda;
    private int difficulty;
    private int total_difficulty;
    private float adrenaline_scale;
    private float hurt_scale;

    [SerializeField]
    private GameObject speedPrefab, invincibilityPrefab;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // initialize the players stats from the Stat_Manager
        stats = Stat_Manager.Instance;
        healthMax = stats.GetMaxHealth();
        adrenalineMax = stats.GetMaxAdrenaline();
        baseSpeed = stats.GetSpeed();
        baseROF = stats.GetROF();
        bullet_size = stats.GetBulletSize();
        baseDamage = stats.GetDamage();
        totalCoins = stats.GetCoins();
        player_lives = stats.GetLives();

        // values that are local to the player object
        healthCurrent = healthMax;
        adrenalineCurrent = adrenalineMax;
        move_speed = baseSpeed;
        rate_of_fire = baseROF;
        damage = baseDamage;

        // update canvas objects 
        lifeUI = GameObject.Find("LifeText").GetComponent<LifeUI>();
        lifeUI.UpdateUI();
        game.SetAdrenaline(adrenalineCurrent / adrenalineMax);
        game.SetHealth(healthCurrent / healthMax);

        audio = gameObject.GetComponent<AudioSource>();

        // setup dynamic diffculty adjustment
        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        dda.Subscribe(this);
        difficulty = dda.GetDifficulty();
        total_difficulty = stats.GetDifficulty() + difficulty;
        ChangeDifficulty(total_difficulty);
    }

    void FixedUpdate()
    {
        if (iFrameCur > 0)
        {
            iFrameCur -= 1;
        }
        if (playPowerup)
        {
            audio.PlayOneShot(powerup, 0.7f);
            playPowerup = false;
        }
        if (playCoin)
        {
            audio.PlayOneShot(coin, 1.5f);
            playCoin = false;
        }
        if(onFire)
        {
            return;
        }
    }

    // Function that changes the player's health by a given amount, 
    // increasing it if positive and decreasing if negative
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
            healthCurrent = Mathf.Min(healthCurrent + num, healthMax);
            game.SetHealth(healthCurrent / healthMax);
        }
    }

    // function that increases player health by a percent value of max health
    public void HealPercent(float percent)
    {
        float healAmount;
        if (percent <= 0)
        {
            if (percent < 0)
            {
                string error = gameObject.name + ".AddHealth() given negative float " + percent;
                Debug.LogError(error);
            }
            return;
        }
        else if (percent > 0)
        {
            healAmount = healthMax * percent;
            healthCurrent = Mathf.Min(healthCurrent + (healthMax * percent), healthMax);
            game.SetHealth(healthCurrent / healthMax);
        }

    }

    public void RemoveHealth(float num)
    {
        if (num <= 0 || (isInvincible || iFrameCur > 0))
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
            healthCurrent -= num*hurt_scale;
            iFrameCur = iFrameMax;
            game.SetHealth(healthCurrent / healthMax);
            audio.PlayOneShot(hit, 0.8f);
            StartCoroutine(DamageFlash());
            if (healthCurrent <= 0)
            {
                player_lives--;
                Stat_Manager.Instance.RemoveLife();
                lifeUI.UpdateUI();
                print("Player Lives Remaining: " + player_lives);
                audio.PlayOneShot(playerDie, 1.0f);
                if (player_lives <= 0)
                {
                    GameOver();
                }
                else
                {
                    Respawn();
                }
            }
        }
    }

    IEnumerator DamageFlash()
    {
        damageCanvas.SetActive(true);
        yield return new WaitForSeconds(.1f);
        damageCanvas.SetActive(false);
    }

    private void Respawn()
    {
        // reset player transform to starting position of level 
        this.transform.position = new Vector3(0, 0, 0);
        healthCurrent = healthMax;
        iFrameCur = 2f * Time.fixedDeltaTime; // 2 seconds of invuln
        gameObject.GetComponent<PulseAbility>().Respawn();

        GameObject speedup = Instantiate(speedPrefab, transform.position, transform.rotation);
        speedup.GetComponent<Speed_up>().duration = 5f;

        GameObject invup = Instantiate(invincibilityPrefab, transform.position, transform.rotation);
        invup.GetComponent<Invincibility_up>().duration = 5f;

        game.SetHealth(healthCurrent / healthMax);
    }

    public void killPlayer()
    {
        healthCurrent = 0;
        game.SetHealth(0);
        if (healthCurrent <= 0)
        {
            GameOver();
        }
    }

    public void AddAdrenaline(float num)
    {
        if (num <= 0)
        {
            if (num < 0)
            {
                string error = gameObject.name + ".AddAdrenaline() given negative float " + num;
                Debug.LogError(error);
            }
            return;
        }
        else if (num > 0)
        {
            adrenalineCurrent = Mathf.Min(adrenalineMax, adrenalineCurrent + (num*adrenaline_scale));
            game.SetAdrenaline(adrenalineCurrent / adrenalineMax);
        }
    }

    public void RemoveAdrenaline(float num)
    {
        if (num <= 0)
        {
            if (num < 0)
            {
                string error = gameObject.name + ".RemoveAdrenaline() given negative float " + num;
                Debug.LogError(error);
            }
            return;
        }
        else
        {
            if (num > adrenalineCurrent)
                Debug.LogError(gameObject.name + ".RemoveAdrenaline given too high value");
            adrenalineCurrent = Mathf.Max(0, adrenalineCurrent - num);
            game.SetAdrenaline(adrenalineCurrent / adrenalineMax);
        }
    }

    public void AddArmour(float num)
    {
        if (num <= 0)
        {
            if (num < 0)
            {
                string error = gameObject.name + ".AddArmour() given negative float " + num;
                Debug.LogError(error);
            }
            return;
        }
        else if (num > 0)
        {
            armourCurrent = Mathf.Min(armourCurrent + num, armourMax);
            //game.SetArmour(armourCurrent / armourMax);
        }
    }

    public void RemoveArmour(float num)
    {
        if (num <= 0)
        {
            if (num < 0)
            {
                string error = gameObject.name + ".RemoveArmour() given negative float " + num;
                Debug.LogError(error);
            }
            return;
        }
        else
        {
            armourCurrent -= num;
            //game.SetArmour(armourCurrent / armourMax);
        }
    }

    public float GetMaxHealth()
    {
        return healthMax;
    }

    // Function to grab the current health of the player
    public float GetHealth()
    {
        return healthCurrent;
    }

    // Function to set the max health of the player
    public void SetHealth(float value)
    {
        healthMax += value;
    }

    // Function to grab the current adrenaline of the player
    public float GetAdrenaline()
    {
        return adrenalineCurrent;
    }

    // Function to get the total coins of the player
    public float GetCoins()
    {
        return totalCoins;
    }

    // Function to set the total coins of the player
    public void SetCoins(float value)
    {
        totalCoins -= value;
    }

    // Function to get the move speed of the player
    public float GetSpeed()
    {
        return move_speed;
    }

    // Function to set the move speed of the player
    public void SetSpeed(float value)
    {
        move_speed += value;
    }

    // Function to get the ROF of the player
    public float GetROF()
    {
        return rate_of_fire;
    }

    // Function to set the ROF of the player
    public void SetROF(float value)
    {
        rate_of_fire += value;
    }

    // Function to get the bullet size of the player
    public float GetBulletSize()
    {
        return bullet_size;
    }

    // Function to set the bullet size of the player
    public void SetBulletSize(float value)
    {
        bullet_size *= value;
    }

    // Function to get the damage of the player
    public float GetDamage()
    {
        return damage;
    }

    // Function to set the damage of the player
    public void SetDamage(float value)
    {
        damage += value;
    }

    // Function to get how many lives the player has remaining
    public int GetLives()
    {
        return player_lives;
    }

    public void AddLife()
    {
        player_lives += 1;
        Stat_Manager.Instance.AddLife();
        lifeUI.UpdateUI();
    }

    // Game over state based on health (may have to make this its own script)
    void GameOver()
    {
        Destroy(gameObject);
        game.EndGame();
    }

    public void refillHealth()
    {
        AddHealth(healthMax);
    }

    public void ChangeDifficulty(int amount)
    {
        difficulty = amount;
        adrenaline_scale = 1.0f + (-0.05f * difficulty);
        hurt_scale = 1.0f + (0.05f * difficulty);
    }

    void OnDestroy()
    {
        dda.Unsubscribe(this);
    }

}
