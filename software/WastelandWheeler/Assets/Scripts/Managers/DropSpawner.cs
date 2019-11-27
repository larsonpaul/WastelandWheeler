using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour, IDiffcultyAdjuster
{
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject tokenPrefab;
    [SerializeField]
    private GameObject healthUp;
    [SerializeField]
    private GameObject lifeUp;

    private int tokenCount;
    public int maxTokens = 10;

    // values used to tune drop rates 
    [SerializeField]
    private float lifeChance = 1.0f;
    [SerializeField]
    private float healChance = 7.0f;
    [SerializeField]
    private float powerupChance = 15.0f;
    [SerializeField]
    private float tokenChance = 50.0f;


    private DynamicDifficultyAdjuster dda;
    private int difficulty;
    // used to control a situation where the player is guaranteed to receive a healing pack when doing very poorly
    private bool guaranteeHeal = false; // turns true if the guaranteed heal has dropped this level
    private Player_stats player;
    float healthPercent;
    private bool lifeDropped = false;

    void Start()
    {
        dda = DynamicDifficultyAdjuster.Instance;
        dda.Subscribe(this);
        difficulty = dda.GetDifficulty();
        maxTokens = Random.Range(10, 16);

        player = GameObject.Find("Player").GetComponent<Player_stats>();
    }

    private void DropLife(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(0f, Vector3.forward);

        GameObject token = Instantiate(lifeUp, t.position, rotation);
    }

    private void DropPowerup(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        int powerupIndex = Random.Range(0, powerups.Length);
        GameObject powerup = Instantiate(powerups[powerupIndex], t.position, rotation);
    }

    private void DropHealth(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(0f, Vector3.forward);

        GameObject token = Instantiate(healthUp, t.position, rotation);
    }

    private void DropToken(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(0f, Vector3.forward);

        GameObject token = Instantiate(tokenPrefab, t.position, rotation);
    }

    public void DropItem(Transform t)
    {
        //lifeChance = 1%;
        //healChance = 7%f;
        //powerupChance = 15%;
        //tokenChance = 50%f;

        // if the player is low health, DDa has made the game easier and this hasn't triggered before
        // drop the healing pack
        healthPercent = player.GetHealth() / player.GetMaxHealth();
        if (difficulty <= -4 && healthPercent < 0.2f && !guaranteeHeal)
        {
            DropHealth(t);
            guaranteeHeal = true;
            return;
        }
        
        // When DDA implemented it should reduce the ranges below
        float chanceValue = (float)Random.Range(1, 100);
        //print(chanceValue);

        if (chanceValue <= lifeChance && !lifeDropped)
        {
            DropLife(t);
            lifeDropped = true;
        }
        else if (chanceValue <= healChance)
        {
            DropHealth(t);
        }
        else if (chanceValue <= powerupChance)
        {
            DropPowerup(t);
        }
        else if (chanceValue <= tokenChance && tokenCount < maxTokens)
        {
            tokenCount++;
            DropToken(t);
            tokenChance = 40.0f;
        }
        tokenChance += 10.0f;
    }

    public void BossDropItem(Transform t)
    {
        int mostlyTokens = 90;

        float chanceValue = (float)Random.Range(1, 100);
        if (chanceValue > mostlyTokens)
        {
            DropLife(t);
        }
        else
        {
            DropToken(t);
        }
    }


        public void ChangeDifficulty(int diff)
    {
        difficulty = diff;
        if (diff <= -4)
        {
            healChance = 10.0f;
        }
        else
        {
            healChance = 5.0f;
        }

        powerupChance = 15.0f - (diff * 2.0f);
      

    }
}
