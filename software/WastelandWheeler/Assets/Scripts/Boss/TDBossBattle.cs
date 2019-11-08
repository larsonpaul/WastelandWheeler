using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDBossBattle : MonoBehaviour
{

    public Transform[] spawnPoints;  //points where boss will move to
    public float speed; // boss's speed
    private int bossPoint; // current point form the array of bossPoints
    private GameObject player;
    private float spawnRate;
    private float spawnMinion;
    public GameObject minions;
    private int minionCount = 0;

    private Rigidbody2D rb;
    public GameObject target; // target/player for boss's aim

    public float bossHealth = 100f;
    //public Transform firePoint; // firePoint for projectiles

    //public GameObject[] barriers; // Create objects with colliders and store in array. Prevents player from leaving area
    private GameObject projectile;  // Boss's projectiles
    //public GameObject projPrefab;
    //public startBossFight startRoutine;       // starts the script once player reaches a certain spot 
    Coroutine bossMethod;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //startRoutine = FindObjectOfType<startBossFight>();
        rb = GetComponent<Rigidbody2D>();
        spawnRate = 10.0f;
        spawnMinion = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        spawnMoreMinions();

    }


    //Spawn enemies at random location
    void spawnMoreMinions()
    {
        //Limit the number of minions
        int maximumSpawn = 3;
        int randomSpawnPoint = Random.Range(0, 2);

        Debug.Log("Generating Spawn at spawnPoint: " + randomSpawnPoint);


        if (Time.time > spawnMinion && minionCount < maximumSpawn)
        {
            Instantiate(minions, spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
            spawnMinion = Time.time + spawnRate;
            minionCount += 1;
        }
    }

}
