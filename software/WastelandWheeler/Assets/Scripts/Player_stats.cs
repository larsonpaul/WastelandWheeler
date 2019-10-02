using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{
    public float healthMax = 100f;
    public float healthCurrent = 100f;
    public float move_speed = 10f;
    public float rate_of_fire = 0.2f;
    public float bullet_size = 2f;

    public bool isInvincible = false;


    public float getHealth()
    {
        return healthCurrent;
    }

    public void addHealth(float num)
    {
        if (num < 0 && isInvincible) return;

        healthCurrent = Mathf.Min(healthCurrent + num, healthMax);
        if (healthCurrent <= 0)
        {
            GameOver();
        }
    }

    public float getSpeed()
    {
        return move_speed;
    }

    public float getROF()
    {
        return rate_of_fire;
    }

    public float getBulletSize()
    {
        return bullet_size;
    }

    void GameOver()
    {
        Destroy(gameObject);
        FindObjectOfType<GameManager>().EndGame();
    }
}
