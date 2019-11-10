﻿using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    // Can add any enemy game object
    public int EnemiesPerWave;
    public GameObject Enemy;
}

public class Spawn_Manager : MonoBehaviour
{
    public Wave[] Waves; // class to hold information per wave

    // 20 Spawns added to prefab for EnemyManager as of 11/2
    public Transform[] SpawnPoints;

    public float TimeBetweenEnemies = 1f;

    [SerializeField]
    private int totalEnemiesInWave;
    [SerializeField]
    private int enemiesLeftInWave;
    [SerializeField]
    private int spawnedEnemies;

    private int currWave;
    [SerializeField]
    private int totalWaves;

    private void Awake()
    {
        enemiesLeftInWave = 0;
    }
    void Start()
    {
        currWave = -1;
        totalWaves = Waves.Length - 1;

        StartNextWave();
    }

    // Begins a wave of enemies
    void StartNextWave()
    {
        currWave++;

        // Win Scenario
        if (currWave >= totalWaves)
        {
            return;
        }

        totalEnemiesInWave = Waves[currWave].EnemiesPerWave;
        enemiesLeftInWave = totalEnemiesInWave;
        spawnedEnemies = 0;
        //print("totalEnemiesInWave" + totalEnemiesInWave);
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        GameObject enemy = Waves[currWave].Enemy;
        while (spawnedEnemies < totalEnemiesInWave)
        {
            spawnedEnemies++;
            //print("spawnedEnemies" + spawnedEnemies);

            int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point.
            Instantiate(enemy, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
            yield return new WaitForSeconds(TimeBetweenEnemies);
        }
        yield return null;
    }

    // called by an enemy when they're defeated (EnemyStats)
    public void EnemyDefeated()
    {
        this.enemiesLeftInWave--;

        // Start the next wave once we have spawned and defeated them all (BUGGY!!!)
        if (enemiesLeftInWave <= 0 && spawnedEnemies == totalEnemiesInWave)
        {
            StartNextWave();
        }
    }
}