using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] powerups;
    [SerializeField]
    public GameObject tokenPrefab;

    public void DropPowerup(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        int powerupIndex = Random.Range(0, powerups.Length);
        GameObject powerup = Instantiate(powerups[powerupIndex], t.position, rotation);
    }

    public void DropToken(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(90f, Vector3.forward);

        GameObject token = Instantiate(tokenPrefab, t.position, rotation);
    }

    public void DropItem(Transform t)
    {
        // When DDA implemented it should reduce the ranges below
        int randInt = Random.Range(0, 101);
        //print(randInt);

        if (randInt <= 10)
            DropPowerup(t);
        else if (randInt > 10 && randInt <= 60)
            DropToken(t);
        else
            return;
    }
}
