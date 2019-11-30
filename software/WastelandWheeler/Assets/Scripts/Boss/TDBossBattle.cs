﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDBossBattle : MonoBehaviour, IDiffcultyAdjuster
{

    public Transform[] spawnPoints;  //points where boss will move to
    public Transform[] bossPoints;

    public float speed; // boss's speed
    public float waitTime = .5f;
    public GameObject boss;
    private Transform bossTrans;

    private int bossPoint; // current point form the array of bossPoints

    private GameObject player;
    public Transform playerPoint;
    public Transform carPoint;

    private float spawnRate;
    private float spawnMinion;
    private int maximumSpawn = 6;
    public GameObject minions;
    private int minionCount = 0;
    private float deathCount;

    public GameObject[] thrownCars;
    private float throwSpeed = 30.0f;
    private float baseThrowSpeed;
    Transform carTarget;
    Vector2 playerTarget;

    public int bossAction = 4;
    float shotTimer;
    float shotReturnTimer = 1.0f;
    Vector3 endShot;

    private Rigidbody2D rb;
    public GameObject target; // target/player for boss's aim

    //calculate player postion
    Vector2 throwAtTarget;
    private bool actionTaken;
    private float actionTime = 10.0f;

    [SerializeField] public float bossHealth;
    [SerializeField] private float maxHealth;
    public Transform firePoint; // firePoint for projectiles

    private GameObject projectile;  // Boss's projectiles
    public GameObject projPrefab;
    public int shots = 5;// number of projecties the boss will fire
    private GameObject[] projArray = new GameObject[5];
    private Vector2[] shotReturns = new Vector2[5];

    Coroutine bossMethod;

    private bool openingScene = true;
    private bool once;
    public bool bossIsDead;

    private Camera mainCam;

    int sawAction = 0;
    public ParticleSystem deathDrop;

    //difficulty caps
    public float baseWaitTime;
    public float baseSpeed;
    public float baseSpawnRate;

    private Stat_Manager stat_manager;
    private int difficulty;
    private DynamicDifficultyAdjuster dda;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //startRoutine = FindObjectOfType<startBossFight>();
        rb = GetComponent<Rigidbody2D>();

        spawnRate = 8.0f;
        spawnMinion = Time.time;

        carTarget = thrownCars[0].GetComponent<Transform>();

        bossHealth = boss.GetComponent<EnemyStats>().health;
        maxHealth = bossHealth;

        mainCam = FindObjectOfType<Camera>();

        bossTrans = boss.GetComponent<Transform>();

        //calculate player postion
        throwAtTarget = target.transform.position - transform.position;
        bossMethod = StartCoroutine(bossOne());

        baseWaitTime = waitTime;
        baseSpeed = speed;
        baseSpawnRate = spawnRate;
        baseThrowSpeed = throwSpeed;

        stat_manager = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
        difficulty = stat_manager.GetDifficulty();
        StartDifficulty(difficulty); // will make enemies harder as player progresses through the game
        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        dda.Subscribe(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (!once && !openingScene)
        {
            spawnMoreMinions();
        }

        //stop coroutine if boss is defeate
        if (!bossIsDead)
        {
            bossHealth = boss.GetComponent<EnemyStats>().health;
            endShot = new Vector3(bossTrans.position.x, bossTrans.transform.position.y, mainCam.transform.position.z);
            if (bossHealth <=0)
            {
                bossIsDead = true;
                StopCoroutine(bossMethod);
                bossDeath();
            }
        }

        if (Time.time > deathCount && bossIsDead)
        {
            endLevel();
        }


        /*if (bossHealth < maxHealth * .40f)
        {
            spawnRate = spawnRate * .50f;
            maximumSpawn = 8;
            Debug.Log("Boss is in danger. Spawn rate " + spawnRate);
        }*/

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
            //Debug.Log("Moving player");
            player.transform.position = Vector2.MoveTowards(player.transform.position,
                new Vector2(playerPoint.transform.position.x, playerPoint.transform.position.y), .07f);
            yield return null;
        }
        //Debug.Log("Stopped moving player");

        yield return new WaitForSeconds(1);

        while (thrownCars[8].transform.position.y != carPoint.transform.position.y)
        {
            Debug.Log("Blocking Exit");
            thrownCars[8].transform.position = Vector2.MoveTowards(thrownCars[8].transform.position,
                new Vector2(carPoint.transform.position.x, carPoint.transform.position.y), 1.0f);
            yield return null;
        }

        //Debug.Log("Exit blocked");

        yield return new WaitForSeconds(waitTime);

        //start the battle
        thrownCars[8].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player.GetComponent<NewPlayerMovementForce>().enabled = true;
        player.GetComponent<PlayerAim>().enabled = true;
        openingScene = false;

        while (bossHealth > 0)
        {

            // pause and then move to location
            bossPoint = actionChoice();
            yield return new WaitForSeconds(waitTime);

            while (bossTrans.position.x != bossPoints[bossPoint].position.x)
            {
                //Debug.Log("Walking to point: " + bossPoint);
                bossTrans.position = Vector2.MoveTowards(bossTrans.position,
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
                    int j = 0;
                    while (j < 5)
                    {
                        shotReturns[j] = fireProjectile(j);
                        yield return new WaitForSeconds(.1f);
                        j++;
                    }

                    yield return new WaitForSeconds(.3f);

                    j = 0;
                    while (j < 5)
                    {
                        returnProjectile(shotReturns[j], j);
                        yield return new WaitForSeconds(.1f);
                        j++;
                    }

                    yield return new WaitForSeconds(.3f);

                    j = 0;
                    while (j < 5)
                    {
                        Destroy(projArray[j]);
                        yield return new WaitForSeconds(.1f);
                        j++;
                    }

                    i--;
                }

                sawAction++;

            }// if sawAction >2

            else if (sawAction >= 1 && bossPoint != 0 &&
                thrownCars[bossPoint - 1].GetComponent<ThrowCar>().thrown == false)
            {
                //walk behind car
                while (bossTrans.position.x != bossPoints[bossPoint + 8].position.x)
                {
                    //Debug.Log("Walking to carPoint: " + bossPoint);
                    bossTrans.position = Vector2.MoveTowards(bossTrans.position,
                    new Vector2(bossPoints[bossPoint + 8].transform.position.x, bossPoints[bossPoint + 8].transform.position.y), speed);
                    yield return null;
                }

                yield return new WaitForSeconds(1);
                //Throw the car aftermaking it dynamic
                thrownCars[bossPoint - 1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                thrownCars[bossPoint - 1].GetComponent<ThrowCar>().collision = false;
                throwCarAtPlayer(bossPoint - 1);

                // walk back from behind car
                while (bossTrans.position.x != bossPoints[bossPoint + 8].position.x)
                {
                    //Debug.Log("Walking back from carpoint to point: " + bossPoint);
                    bossTrans.position = Vector2.MoveTowards(bossTrans.position,
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
            //EnemyStats bossStat = GetComponent<EnemyStats>();
            Object[] leftOverBullets = FindObjectsOfType(typeof(bossBullet));
            GameObject[] leftOverSpawn = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject s in leftOverSpawn)
            {
                s.GetComponent<EnemyStats>().OnDeath();
            }

            foreach (Object b in leftOverBullets)
            {
                Destroy(b);
            }

            //center camera on boss
            Debug.Log("Moving camera");
            //Vector3 moveCam = transform.position - mainCam.transform.position;
            mainCam.transform.position = endShot;
            deathCount = Time.time + 7.0f;
            once = true;
        }
    }


    //upgrade scene
    void endLevel()
    {
        Debug.Log("Loading next scene");
        GameObject.Find("StatManager").GetComponent<Stat_Manager>().EndOfLevel();
        SceneManager.LoadScene(1);
    }

    // fire boss saw blade
    Vector2 fireProjectile(int index)
    {
        Vector2 trgt = (target.transform.position - bossTrans.position).normalized * 30;
        projArray[index] = Instantiate(projPrefab, firePoint.position, Quaternion.identity) as GameObject;
        projArray[index].GetComponent<Rigidbody2D>().AddForce(trgt, ForceMode2D.Impulse);
        return trgt;
    }

    //return saw blade
    void returnProjectile(Vector2 vect, int index)
    {
        //Vector2 trgt = transform.position - target.transform.position;
        projArray[index].GetComponent<Rigidbody2D>().velocity = new Vector2(-vect.x, -vect.y);
    }


    // Random action for boss
    public int actionChoice()
    {
        int randomChoice = Random.Range(0, 8);

        if (randomChoice >= 0 && randomChoice < 8)
        {
            //Debug.Log("Boss Action: " + randomChoice);
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
        //Debug.Log("minionCount = " + spawnedMinions);
        int randomSpawnPoint = Random.Range(0, 4);

        //Debug.Log("Generating Spawn at spawnPoint: " + randomSpawnPoint);

        if (Time.time > spawnMinion && minionCount < maximumSpawn)
        {
            Instantiate(minions, spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
            spawnMinion = Time.time + spawnRate;
            minionCount += 1;
        }
        else if (Time.time > spawnMinion && minionCount >= maximumSpawn)
        {
            //Debug.Log("Maximum spawn = " + minionCount);
        }
        else if (Time.time > spawnMinion)
        {
            Debug.Log("Error in spawnMoreMinions. Max Spawn:" + maximumSpawn);
        }

    }

    void throwCarAtPlayer(int carNumber)
    {
        //Debug.Log("Throwing car" + carNumber);
        //calculate player postion
        throwAtTarget = (target.transform.position - bossTrans.position).normalized;

        //apply force to car Object 
        Rigidbody2D carRB = thrownCars[carNumber].GetComponent<Rigidbody2D>();
        carRB.velocity = new Vector2(throwAtTarget.x, throwAtTarget.y) * throwSpeed;
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

        if (throwSpeed > 10.0f)
        {
            throwSpeed = baseThrowSpeed + (difficulty * 2);
            Debug.Log("Throw Speed: " + throwSpeed);
        }
    }
}
