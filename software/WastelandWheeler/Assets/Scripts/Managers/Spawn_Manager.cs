﻿using System.Collections;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{

    public GameObject[] EnemyTypes; // holds the prefabs for the types of enemies that can be spawned
    public GameObject[] MeleeEnemies;
    public GameObject[] RangedEnemies;

    // 20 Spawns added to prefab for EnemyManager as of 11/2
    private Transform[] SpawnPoints;

    private int currWave;
    [SerializeField]
    private GameObject[] typeEnemies;

    // parameters used by difficulty adjustment system to make the game harder as player progresses through levels
    private int difficulty;
    [SerializeField]
    int num_waves;
    [SerializeField]
    int enemy_types_per_wave;
    [SerializeField]
    [Range(6f, 100f)]
    private int totalEnemiesInWave;
    [SerializeField]
    private int enemiesLeftInWave;
    [SerializeField]
    private int spawnedEnemies;
    [SerializeField]
    public float TimeBetweenEnemies = 1.0f;

    private GameManager gameManager;

    private void Awake()
    {
        SpawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            SpawnPoints[i] = transform.GetChild(i);
        }

        enemiesLeftInWave = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        if (!gameManager.isArena && !gameManager.isBoss)
            num_waves = 1;

        currWave = 0;

        DifficultySetup();

        StartNextWave();
    }

    // Method to set parameters based on difficulty measure
    void DifficultySetup()
    {
        difficulty = GameObject.Find("StatManager").GetComponent<Stat_Manager>().GetDifficulty();
        if (difficulty <= 3)
        {
            enemy_types_per_wave = 2;
            TimeBetweenEnemies = 1.0f;
            totalEnemiesInWave = 20;
        }   
        else if (difficulty <= 7)
        {
            enemy_types_per_wave = 3;
            TimeBetweenEnemies = Random.Range(0.5f, 0.9f);
            totalEnemiesInWave = 30;
        }
        else
        {
            enemy_types_per_wave = 4;
            TimeBetweenEnemies = Random.Range(0.5f, 0.9f);
            totalEnemiesInWave = 40;
        }
    }

    // Begins a wave of enemies
    void StartNextWave()
    {
        currWave++;
        // Win Scenario
        if (currWave > num_waves)
        {
            Debug.Log("SpawnManager: All enemies cleared");
            gameManager.EnemiesCleared();
            return;
        }


        // setup which enemies can spawn this round
        typeEnemies = new GameObject[enemy_types_per_wave];
        typeEnemies[0] = MeleeEnemies[Random.Range(0, MeleeEnemies.Length)];
        typeEnemies[1] = RangedEnemies[Random.Range(0, RangedEnemies.Length)];

        int last = -1;
        int choice = -1;
        for (int i = 2; i < enemy_types_per_wave; i++)
        {
            while (choice == last)
            {
                choice = Random.Range(0, EnemyTypes.Length);
            }
            typeEnemies[i] = EnemyTypes[choice];
            last = choice;
        }
        
        enemiesLeftInWave = totalEnemiesInWave;
        spawnedEnemies = 0;
        //print("totalEnemiesInWave" + totalEnemiesInWave);
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        // for now we are going to try spawning jsut random enemies

        GameObject enemy;
        while (spawnedEnemies < totalEnemiesInWave)
        {
            enemy = typeEnemies[Random.Range(0, enemy_types_per_wave)]; // randomly spawn from the sublist of available enemies
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
            DifficultySetup();
            StartNextWave();
        }
    }
}