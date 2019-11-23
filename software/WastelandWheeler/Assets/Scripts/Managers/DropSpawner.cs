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

    private DynamicDifficultyAdjuster dda;
    private int difficulty;

    // values used to tune drop rates 
    [SerializeField]
    private float lifeChance = 1.0f;
    [SerializeField]
    private float healChance = 7.0f;
    [SerializeField]
    private float powerupChance = 15.0f;
    [SerializeField]
    private float tokenChance = 50.0f;

    void Start()
    {
        dda = DynamicDifficultyAdjuster.Instance;
        dda.Subscribe(this);
        difficulty = dda.GetDifficulty();
        maxTokens = Random.Range(10, 16);
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

    public void DropToken(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(90f, Vector3.forward);

        GameObject token = Instantiate(tokenPrefab, t.position, rotation);
    }

    public void DropItem(Transform t)
    {
        //lifeChance = 1.0f;
        //healChance = 5.0f;
        //powerupChance = 20.0f;
        //tokenChance = 50.0f;
        
        // When DDA implemented it should reduce the ranges below
        float chanceValue = (float)Random.Range(1, 100);
        //print(chanceValue);

        if (chanceValue <= lifeChance)
        {
            DropLife(t);
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

    public void ChangeDifficulty(int diff)
    {
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
