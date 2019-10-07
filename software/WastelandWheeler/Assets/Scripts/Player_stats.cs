using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that contains varaibles that control player attributes and has getters for those variables
 */
public class Player_stats : MonoBehaviour
{
    public float healthMax = 100f;
    public float healthCurrent = 100f;
    public float move_speed = 10f;
    public float rate_of_fire = 0.2f;
    public float bullet_size = 2f;
    public bool isInvincible = false;

    public float iFrameMax = 20f;
    public float iFrameCur = 0f;

    [SerializeField] private GameManager game;


    void Update()
    {
        if (iFrameCur > 0)
        {
            iFrameCur -= 1;
        }
    }

    // Function to grab the current health of the player
    public float GetHealth()
    {
        return healthCurrent;
    }

    // Function that changes the player's health by a given amount, 
    // increasing it if positive and decreasing if negative
    public void AddHealth(float num)
    {
        if (num < 0)
        {
            Debug.LogError("AddHealth() given negative float " + num);
            return;
        }
        healthCurrent = Mathf.Min(healthCurrent + num, healthMax);
        game.SetHealth(healthCurrent / healthMax);
    }

    public void RemoveHealth(float num)
    {
        if (num < 0)
        {
            Debug.LogError("RemoveHealth() given negative float " + num);
            return;
        }
        if (isInvincible || iFrameCur > 0) return;
        if (num > 0)
        {
            healthCurrent -= num;
            iFrameCur = iFrameMax;
        }
        if (healthCurrent <= 0) GameOver();
        game.SetHealth(healthCurrent / healthMax);
    }

    // Function to get the move speed of the player
    public float GetSpeed()
    {
        return move_speed;
    }

    // Function to get the ROF of the player
    public float GetROF()
    {
        return rate_of_fire;
    }

    // Function to get the bullet size of the player
    public float GetBulletSize()
    {
        return bullet_size;
    }

    // Game over state based on health (may have to make this its own script)
    void GameOver()
    {
        Destroy(gameObject);
        game.EndGame();
    }
}
