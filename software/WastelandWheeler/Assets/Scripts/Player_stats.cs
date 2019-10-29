using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that contains varaibles that control player attributes and has getters for those variables
 */
public class Player_stats : MonoBehaviour, IDiffcultyAdjuster
{
    public float healthMax = 100f;
    public float healthCurrent = 100f;

    public float baseSpeed = 50f;
    public float move_speed = 50f;

    public float baseROF = 0.2f;
    public float rate_of_fire = 0.2f;

    public float bullet_size = 2f;
    public bool isInvincible = false;
    public float adrenalineMax = 100f;
    public float adrenalineCurrent = 100f;

    public float iFrameMax = 20f;
    public float iFrameCur = 0f;
    public bool levelIsTopDown = true;
    public LevelManager levelManager;

    [SerializeField]
    private GameManager game;

    [SerializeField]
    private GameObject speedIcon, rofIcon, invincibleIcon;

    private GameObject dda;
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        dda = GameObject.Find("DDA");
        dda.GetComponent<DynamicDifficultyAdjuster>().Subscribe(this);
    }

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
            healthCurrent -= num;
            iFrameCur = iFrameMax;
            game.SetHealth(healthCurrent / healthMax);
            if (healthCurrent <= 0 && levelIsTopDown)
            {
                GameOver();
            }
            else if (healthCurrent <= 0 && !levelIsTopDown)
            {
                Debug.Log("Respawn");
                levelManager.respawnPlayer();
                refillHealth();

            }
        }
    }

    public void killPlayer()
    {
        healthCurrent = 0;
        game.SetHealth(0);
        if (healthCurrent <= 0 && levelIsTopDown)
        {
            GameOver();
        }
        else if (healthCurrent <= 0 && !levelIsTopDown)
        {
            Debug.Log("Respawn");
            levelManager.respawnPlayer();
            refillHealth();

        }
    }

    public void AddAdrenaline(float num)
    {
        // Add Code Here
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
            adrenalineCurrent = Mathf.Min(adrenalineCurrent + num, adrenalineMax);
            game.SetAdrenaline(adrenalineCurrent / adrenalineMax);
        }
    }

    public void RemoveAdrenaline(float num)
    {
        // Add Code Here
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

    public IEnumerator PowerSpeed(float amount, float duration)
    {
        speedIcon.SetActive(true);

        move_speed *= amount;

        yield return new WaitForSeconds(duration);

        move_speed = baseSpeed;
        speedIcon.SetActive(false);
    }

    public IEnumerator PowerROF(float amount, float duration)
    {
        rofIcon.SetActive(true);

        rate_of_fire /= amount;

        yield return new WaitForSeconds(duration);

        rate_of_fire = baseROF;
        rofIcon.SetActive(false);
    }

    public IEnumerator PowerInvincible(float duration)
    {
        invincibleIcon.SetActive(true);

        isInvincible = true;

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        invincibleIcon.SetActive(false);
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
        dda.GetComponent<DynamicDifficultyAdjuster>().Unsubscribe(this);
        Destroy(gameObject);
        game.EndGame();
    }

    public void refillHealth()
    {
        AddHealth(healthMax);
    }

    public void ChangeDifficulty(float amount)
    {
        throw new System.NotImplementedException();
    }
}
