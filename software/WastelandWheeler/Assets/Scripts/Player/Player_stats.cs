using System.Collections;
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

    public float baseSpeed = 50f;
    public float move_speed = 50f;

    public float baseROF = 0.2f;
    public float rate_of_fire = 0.2f;

    public float bullet_size = 1f;

    public bool isInvincible = false;
    public bool isThorny = false;
    public bool tripleShot = false;
    public bool isArmoured = false;
    public float dmgReduction = 0.25f;

    public float adrenalineMax = 100f;
    public float adrenalineCurrent = 100f;

    public float iFrameMax = 20f;
    public float iFrameCur = 0f;

    public float baseDamage = 5f;
    public float damage = 5f;

    public float totalCoins = 0f;

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

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // initialize the players stats from the Stat_Manager
        stats = Stat_Manager.Instance;
        healthMax = stats.GetMaxHealth();
        healthCurrent = stats.GetMaxHealth(); // change this if the player health is not reseting on level load
        baseSpeed = stats.GetSpeed();
        move_speed = baseSpeed;
        baseROF = stats.GetROF();
        rate_of_fire = baseROF;
        bullet_size = stats.GetBulletSize();
        adrenalineMax = stats.GetMaxAdrenaline();
        adrenalineCurrent = stats.GetCurrentAdrenaline();
        baseDamage = stats.GetDamage();
        damage = baseDamage;
        totalCoins = stats.GetCoins();
        player_lives = stats.GetLives();
        lifeUI = GameObject.Find("LifeText").GetComponent<LifeUI>();

        // setup dynamic diffculty adjustment
        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        dda.Subscribe(this);
        difficulty = dda.GetDifficulty();
        total_difficulty = stats.GetDifficulty() + difficulty;
        ChangeDifficulty(total_difficulty);
    }

    void Update()
    {
        if (iFrameCur > 0)
        {
            iFrameCur -= 1;
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
            if (isArmoured)
            {
                num *= dmgReduction;
            }
            healthCurrent -= num*hurt_scale;
            iFrameCur = iFrameMax;
            game.SetHealth(healthCurrent / healthMax);
            if (healthCurrent <= 0)
            {
                player_lives--;
                print("player Lives Remaining: " + player_lives);
                Respawn();
            }
            if (player_lives <= 0)
            {
                GameOver();
            }
        }
    }

    public void Respawn()
    {
        // reset player transform to starting position of level 
        this.transform.position = new Vector3(0, 0, 0);
        healthCurrent = healthMax;
        game.SetHealth(healthCurrent / healthMax);
        lifeUI.UpdateUI();

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
            adrenalineCurrent = Mathf.Min(adrenalineCurrent + (num*adrenaline_scale), adrenalineMax);
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
            adrenalineCurrent -= num;
            game.SetAdrenaline(adrenalineCurrent / adrenalineMax);
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

    // Game over state based on health (may have to make this its own script)
    void GameOver()
    {
        dda.GetComponent<DynamicDifficultyAdjuster>().Unsubscribe(this);
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
        if (difficulty <= 0)
        {
            rate_of_fire = baseROF * (1.0f + (0.025f * difficulty));
        }
        else
        {
            rate_of_fire = baseROF;
        }
        hurt_scale = 1.0f + (0.1f * difficulty);
    }
}
