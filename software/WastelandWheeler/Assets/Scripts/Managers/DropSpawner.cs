using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
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

    private void DropLife(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(90f, Vector3.forward);

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
        Quaternion rotation = Quaternion.AngleAxis(90f, Vector3.forward);

        GameObject token = Instantiate(healthUp, t.position, rotation);
    }

    private void DropToken(Transform t)
    {
        Quaternion rotation = Quaternion.AngleAxis(90f, Vector3.forward);

        GameObject token = Instantiate(tokenPrefab, t.position, rotation);
    }

    public void DropItem(Transform t)
    {
        // When DDA implemented it should reduce the ranges below
        int randInt = Random.Range(1, 100);
        print(randInt);

        if (randInt <= 2)
        {
            DropLife(t);
        }
        else if (randInt <= 12)
        {
            DropPowerup(t);
        }
        else if (randInt <= 20)
        {
            DropHealth(t);
        }
        else if (randInt <= 52)
        {
            DropToken(t);
        }
    }
}
