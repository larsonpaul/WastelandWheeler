using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDBossBattle : MonoBehaviour, IDiffcultyAdjuster
{

    public Transform[] spawnPoints;  //points where boss will move to
    public Transform[] bossPoints;

    public float speed; // boss's speed
    public float waitTime = .5f;

    private int bossPoint; // current point form the array of bossPoints

    private GameObject player;
    public Transform playerPoint;
    public Transform carPoint;

    private float spawnRate;
    private float spawnMinion;
    private int maximumSpawn = 3;
    public GameObject minions;
    private int minionCount = 0;
    private float deathCount;

    public GameObject[] thrownCars;
    Transform carTarget;
    Vector2 playerTarget;

    public int bossAction = 4;
    float shotTimer;
    float shotReturnTimer = 1.0f;

    private Rigidbody2D rb;
    public GameObject target; // target/player for boss's aim

    //calculate player postion
    Vector2 throwAtTarget;
    private bool actionTaken;
    private float actionTime = 10.0f;

    [SerializeField] private float bossHealth;
    [SerializeField] private float maxHealth;
    public Transform firePoint; // firePoint for projectiles

    private GameObject projectile;  // Boss's projectiles
    public GameObject projPrefab;
    Coroutine bossMethod;

    private bool openingScene = true;
    private bool once;

    private Camera mainCam;

    int sawAction = 0;
    public ParticleSystem deathDrop;

    //difficulty caps
    public float baseWaitTime;
    public float baseSpeed;
    public float baseSpawnRate;

    private Stat_Manager stat_manager;
    private int difficulty;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //startRoutine = FindObjectOfType<startBossFight>();
        rb = GetComponent<Rigidbody2D>();
        spawnRate = 10.0f;
        spawnMinion = Time.time;
        carTarget = thrownCars[0].GetComponent<Transform>();
        bossHealth = GetComponent<EnemyStats>().health;
        maxHealth = bossHealth;
        mainCam = FindObjectOfType<Camera>();

        //calculate player postion
        throwAtTarget = target.transform.position - transform.position;
        bossMethod = StartCoroutine(bossOne());

        baseWaitTime = waitTime;
        baseSpeed = speed;
        baseSpawnRate = spawnRate;

        stat_manager = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
        difficulty = stat_manager.GetDifficulty();
        StartDifficulty(difficulty); // will make enemies harder as player progresses through the game

    }

    // Update is called once per frame
    void Update()
    {
        if (!once && !openingScene)
        {
            spawnMoreMinions();
        }

        //stop coroutine if boss is defeated
        bossHealth = GetComponent<EnemyStats>().health;

        if (bossHealth <= 0)
        {
            StopCoroutine(bossMethod);
            bossDeath();
        }

        if (bossHealth < maxHealth * .5)
        {
            spawnRate = spawnRate/2;
            maximumSpawn = 4;
        }

    }

    // Coroutine for boss actions
    IEnumerator bossOne()
    {

        //Opening scene - move player into arena
        while (player.transform.position.y != playerPoint.transform.position.y)
        {
            player.GetComponent<NewPlayerMovementForce>().enabled = false;
            player.GetComponent<PlayerAim>().enabled = false;
            //mainCam.GetComponent<CameraMove>().enabled = false;
            Debug.Log("Moving player");
            player.transform.position = Vector2.MoveTowards(player.transform.position,
                new Vector2(playerPoint.transform.position.x, playerPoint.transform.position.y), .07f);
            yield return null;
        }
        Debug.Log("Stopped moving player");

        yield return new WaitForSeconds(1);

        while (thrownCars[4].transform.position.y != carPoint.transform.position.y)
        {
            Debug.Log("Blocking Exit");
            thrownCars[4].transform.position = Vector2.MoveTowards(thrownCars[4].transform.position,
                new Vector2(carPoint.transform.position.x, carPoint.transform.position.y), 1.0f);
            yield return null;
        }

        Debug.Log("Exit blocked");

        yield return new WaitForSeconds(waitTime);

        //start the battle
        thrownCars[4].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player.GetComponent<NewPlayerMovementForce>().enabled = true;
        player.GetComponent<PlayerAim>().enabled = true;
        openingScene = false;

        while (bossHealth > 0)
        {

            // pause and then move to location
            //yield return new WaitForSeconds(5);
            bossPoint = actionChoice();
            yield return new WaitForSeconds(waitTime);

            while (transform.position.x != bossPoints[bossPoint].position.x)
            {
                Debug.Log("Walking to point: " + bossPoint);
                transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(bossPoints[bossPoint].transform.position.x, bossPoints[bossPoint].transform.position.y), speed);
                yield return null;
            }

            //pause then 3 fire projectiles
            yield return new WaitForSeconds(.5f);


            if (sawAction < 1)
            {
                int i = 3;
                while (i > 0)
                {
                    Vector2 vect = fireProjectile();
                    yield return new WaitForSeconds(1);
                    returnProjectile(vect);
                    yield return new WaitForSeconds(1);
                    Destroy(projectile);
                    i--;
                }

                sawAction++;

            }// if sawAction >2

            else if (sawAction >= 1 && bossPoint != 0 &&
                thrownCars[bossPoint - 1].GetComponent<ThrowCar>().thrown == false)
            {
                //walk behind car
                while (transform.position.x != bossPoints[bossPoint + 4].position.x)
                {
                    Debug.Log("Walking to carPoint: " + bossPoint);
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint + 4].transform.position.x, bossPoints[bossPoint + 4].transform.position.y), speed);
                    yield return null;
                }

                yield return new WaitForSeconds(1);
                //Throw the car aftermaking it dynamic
                thrownCars[bossPoint - 1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                thrownCars[bossPoint - 1].GetComponent<ThrowCar>().collision = false;
                throwCarAtPlayer(bossPoint - 1);

                // walk back from behind car
                while (transform.position.x != bossPoints[bossPoint + 4].position.x)
                {
                    Debug.Log("Walking back from carpoint to point: " + bossPoint);
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint].transform.position.x, bossPoints[bossPoint].transform.position.y), speed);
                    yield return null;
                }

                thrownCars[bossPoint - 1].GetComponent<ThrowCar>().thrown = true;
                sawAction = 0;

            }// else if - walk to car

            else
            {
                //start throwing saws again
                sawAction = 0;
            }

        }// while boss health > 0

        yield return null;
    }

    void bossDeath()
    {
        //start count down until scene change
        if (Time.time > deathCount && !once)
        {
            mainCam.GetComponent<CameraMove>().enabled = false;
            EnemyStats bossStat = GetComponent<EnemyStats>();
            //bossStat.enabled = false;
            //bossStat.RemoveHealth(bossStat.health);
            Object[] leftOverBullets = FindObjectsOfType(typeof(bossBullet));
            GameObject[] leftOverSpawn = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (Object s in leftOverSpawn)
            {
                Destroy(s);
            }

            foreach (Object b in leftOverBullets)
            {
                Destroy(b);
            }

            //center camera on boss
            Debug.Log("Moving camera");
            //Vector3 moveCam = transform.position - mainCam.transform.position;
            mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, mainCam.transform.position.z);
            deathCount = Time.time + 7.0f;
            once = true;
        }

        //upgrade scene
        else if (Time.time > deathCount)
        {
            Debug.Log("Loading next scene");
            GameObject.Find("StatManager").GetComponent<Stat_Manager>().EndOfLevel();
            SceneManager.LoadScene(1);
        }

    }
    // fire boss saw blade
    Vector2 fireProjectile()
    {
        Vector2 trgt = (target.transform.position - transform.position).normalized * 12;
        projectile = Instantiate(projPrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce(trgt, ForceMode2D.Impulse);
        return trgt;

    }

    //return saw blade
    void returnProjectile(Vector2 vect)
    {
        //Vector2 trgt = transform.position - target.transform.position;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-vect.x, -vect.y);
    }


    // Random action for boss
    public int actionChoice()
    {
        int randomChoice = Random.Range(0, 4);

        if (randomChoice >= 0 && randomChoice < 4)
        {
            Debug.Log("Boss Action: " + randomChoice);
        }
        else
        {
            Debug.Log("Boss choice out of range");
        }

        return randomChoice;
    }

    //Spawn enemies at random location
    void spawnMoreMinions()
    {
        //Limit the number of minions
        GameObject[] spawnedMinions = GameObject.FindGameObjectsWithTag("Enemy");
        minionCount = spawnedMinions.Length;
        Debug.Log("minionCount = " + spawnedMinions);
        int randomSpawnPoint = Random.Range(0, 2);

        Debug.Log("Generating Spawn at spawnPoint: " + randomSpawnPoint);

        if (Time.time > spawnMinion && minionCount < maximumSpawn)
        {
            Instantiate(minions, spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
            spawnMinion = Time.time + spawnRate;
            minionCount += 1;
        }
        else if (Time.time > spawnMinion && minionCount >= maximumSpawn)
        {
            Debug.Log("Maximum spawn = " + minionCount);
        }
        else if (Time.time > spawnMinion)
        {
            Debug.Log("Error in spawnMoreMinions. Max Spawn:" + maximumSpawn);
        }

    }

    void throwCarAtPlayer(int carNumber)
    {
        Debug.Log("Throwing car" + carNumber);
        //calculate player postion
        throwAtTarget = (target.transform.position - transform.position).normalized;

        //apply force to car Object 
        Rigidbody2D carRB = thrownCars[carNumber].GetComponent<Rigidbody2D>();
        carRB.velocity = new Vector2(throwAtTarget.x, throwAtTarget.y) * 30.0f;
        thrownCars[carNumber].GetComponent<ThrowCar>().thrown = true;

    }

    public void StartDifficulty(int difficulty)
    {
        float difficulty_mod = (1 + 0.1f * difficulty);

    }

    public void ChangeDifficulty(int difficulty)
    {
        if (speed > 0)
        {
            speed = baseSpeed + (0.05f * difficulty);
            Debug.Log("speed: " + speed);
        }
        else
        {
            Debug.Log("Speed cant't be less than 0");
        }

            spawnRate = baseSpawnRate - (.5f * difficulty);
            Debug.Log("Spawn rate now: " + spawnRate);

        if (waitTime > 0)
        {
            waitTime = baseWaitTime - (difficulty / 10.0f);
            Debug.Log("Wait time: " + waitTime);
        }
        else
        {
            Debug.Log("Wait time cant't be less than 0");
        }
    }
}
