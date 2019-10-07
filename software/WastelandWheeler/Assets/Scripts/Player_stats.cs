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

    [SerializeField] private GameManager game;

    // Function to grab the current health of the player
    public float getHealth()
    {
        return healthCurrent;
    }

    // Function that changes the player's health by a given amount, 
    // increasing it if positive and decreasing if negative
    public void addHealth(float num)
    {
        if (num < 0 && isInvincible)
            return;
        healthCurrent = Mathf.Min(healthCurrent + num, healthMax);
        if (healthCurrent <= 0)
        {
            GameOver();
        }
        game.SetHealth(healthCurrent / healthMax);
    }

    // Function to get the move speed of the player
    public float getSpeed()
    {
        return move_speed;
    }

    // Function to get the ROF of the player
    public float getROF()
    {
        return rate_of_fire;
    }

    // Function to get the bullet size of the player
    public float getBulletSize()
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
