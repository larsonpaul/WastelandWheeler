using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Manager : MonoBehaviour
{
    // next two functions are to keep player stats between scenes 
    public static Stat_Manager Instance
    {
        get;
        set;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // base stats for the game that need to persist between levels
    private float max_health = 100f;
    private float cur_health = 100f;
    private float speed = 60f;
    private float rate_of_fire = .2f;
    private float damage = 5f;
    private float bullet_size = 1f;
    private float max_adrenaline = 100f;
    private float cur_adrenaline = 100f;
    private float coins = 0;
    private int lives = 5;

    private int persistent_difficulty = 0;

    private Player_stats player;

    private int end_level_difficulty;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Function to grab the base health of the player
    public float GetMaxHealth()
    {
        return max_health;
    }

    // Function to increase the max health of the player
    public void SetMaxHealth(float value)
    {
        max_health += value;
    }

    // Function to grab the current health of the player
    public float GetCurrentHealth()
    {
        return cur_health;
    }

    // Function to set the current health of the player
    public void SetCurrentHealth(float value)
    {
        cur_health = value;
    }

    // Function to grab the current adrenaline of the player
    public float GetMaxAdrenaline()
    {
        return max_adrenaline;
    }

    // Function to set how much adrenaline the player has
    public void SetMaxAdrenaline(float value)
    {
        max_adrenaline = value;
    }

    // Function to grab the current adrenaline of the player
    public float GetCurrentAdrenaline()
    {
        return cur_adrenaline;
    }

    // Function to set how much adrenaline the player has
    public void SetCurrentAdrenaline(float value)
    {
        cur_adrenaline = value;
    }

    // Function to get the total coins of the player
    public float GetCoins()
    {
        return coins;
    }

    // Function to set the total coins of the player
    public void SetCoins(float value)
    {
        coins -= value;
    }

    // Function to get the move speed of the player
    public float GetSpeed()
    {
        return speed;
    }

    // Function to set the move speed of the player
    public void SetSpeed(float value)
    {
        speed += value;
    }

    // Function to get the ROF of the player
    public float GetROF()
    {
        return rate_of_fire;
    }

    // Function to set the ROF of the player
    public void SetROF(float value)
    {
        rate_of_fire *= value;
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

    // Function to get the damage of the player
    public int GetLives()
    {
        return lives;
    }

    public void AddLife()
    {
        lives++;
    }

    // Function to set the damage of the player
    public void RemoveLife()
    {
        lives--;
    }

    // Function to get the current difficult of the game 
    // this is an increasing value that as the palyer progresses should make the game harder
    public int GetDifficulty()
    {
        return persistent_difficulty;
    }

    // Function to set the current difficult of the game 
    public void SetDifficulty(int value)
    {
        persistent_difficulty = value;
    }

    // method called when the level so that the Stat_Manager can read and save values from the current level
    public void EndOfLevel()
    {
        player = GameObject.Find("Player").GetComponent<Player_stats>();
        lives = player.GetLives();
        coins = player.GetCoins();
        cur_adrenaline = player.GetAdrenaline();
        // modify how hard the next wave will be 
        end_level_difficulty = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>().GetDifficulty();
        if (end_level_difficulty >= 5) { // player did very well
            persistent_difficulty +=2;
        }
        else if (end_level_difficulty > -5) // player did OK
        {
            persistent_difficulty++;
        } // persistent difficulty doesn't change if the DDA reached the easiest scale
    }
}
