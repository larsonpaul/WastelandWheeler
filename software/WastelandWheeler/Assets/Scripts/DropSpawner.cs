using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] powerups;
    [SerializeField]
    public GameObject tokenPrefab;

    public void DropPowerup()
    {
        Quaternion rotation = Quaternion.AngleAxis(0f, Vector3.forward);

        int powerupIndex = Random.Range(0, powerups.Length);
        GameObject powerup = Instantiate(powerups[powerupIndex], transform.position, rotation);
    }

    public void DropToken()
    {
        Quaternion rotation = Quaternion.AngleAxis(90f, Vector3.forward);

        GameObject token = Instantiate(tokenPrefab, transform.position, rotation);
    }

    public void DropItem()
    {
        // When DDA implemented it should reduce the ranges below
        int randInt = Random.Range(0, 101);
        print(randInt);

        if (randInt <= 10)
            DropPowerup();
        else if (randInt > 10 && randInt <= 60)
            DropToken();
        else
            return;
    }
}
