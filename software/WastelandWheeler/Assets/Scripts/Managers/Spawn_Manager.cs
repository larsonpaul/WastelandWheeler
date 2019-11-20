using System.Collections;
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
    public GameObject[] EnemyTypes; // holds the prefabs for the types of enemies that can be spawned

    // 20 Spawns added to prefab for EnemyManager as of 11/2
    public Transform[] SpawnPoints;

    private int currWave;
    private GameObject[] typeEnemies;

    // parameters used by difficulty adjustment system to make the game harder as player progresses through levels
    private int difficulty;
    [SerializeField]
    int num_waves = 1;
    [SerializeField]
    int enemy_types_per_wave;
    [SerializeField]
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
        enemiesLeftInWave = 0;
    }
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        currWave = -1;

        DifficultySetup();

        StartNextWave();
    }

    // Method to set parameters based on difficulty measure
    void DifficultySetup()
    {
        difficulty = GameObject.Find("StatManager").GetComponent<Stat_Manager>().GetDifficulty();
        if (difficulty < 3)
        {
            enemy_types_per_wave = 2;
            TimeBetweenEnemies = 1.0f;
            totalEnemiesInWave = 15;
        }   
        else
        {
            enemy_types_per_wave = Random.Range(2, 5);
            TimeBetweenEnemies = ((float)Random.Range(5, 9)) / 10;
            totalEnemiesInWave = Random.Range(15, 26);
        }
    }

    // Begins a wave of enemies
    void StartNextWave()
    {
        currWave++;
        // Win Scenario
        if (currWave >= num_waves)
        {
            gameManager.EnemiesCleared();
            return;
        }


        // setup which enemies can spawn this round
        typeEnemies = new GameObject[enemy_types_per_wave];
        int last = -1;
        int choice = -1;
        for (int i = 0; i < enemy_types_per_wave; i++)
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
            StartNextWave();
        }
    }
}