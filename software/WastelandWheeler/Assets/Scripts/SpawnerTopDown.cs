using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTopDown : MonoBehaviour
{
    public GameObject enemy;
    public bool respawn;
    public float spawnDelay;
    private float currentTime;
    private bool spawning;

    void Start()
    {
        Spawn();
        currentTime = spawnDelay;
    }

    void Update()
    {
        if (spawning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Spawn();
            }
        }
    }

    public void Respawn()
    {
        spawning = true;
        currentTime = spawnDelay;
    } 

    void Spawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        spawning = false;
    }
}
