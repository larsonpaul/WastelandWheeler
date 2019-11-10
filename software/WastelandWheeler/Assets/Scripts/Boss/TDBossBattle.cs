using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDBossBattle : MonoBehaviour
{

    public Transform[] spawnPoints;  //points where boss will move to
    public Transform[] bossPoints;
    public float speed; // boss's speed
    private int bossPoint; // current point form the array of bossPoints

    private GameObject player;

    private float spawnRate;
    private float spawnMinion;
    public GameObject minions;
    private int minionCount = 0;

    public GameObject [] thrownCars;
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

    public float bossHealth = 100f;
    public Transform firePoint; // firePoint for projectiles

    //public GameObject[] barriers; // Create objects with colliders and store in array. Prevents player from leaving area
    private GameObject projectile;  // Boss's projectiles
    public GameObject projPrefab;
    //public startBossFight startRoutine;       // starts the script once player reaches a certain spot 
    Coroutine bossMethod;

    int sawAction = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //startRoutine = FindObjectOfType<startBossFight>();
        rb = GetComponent<Rigidbody2D>();
        spawnRate = 10.0f;
        spawnMinion = Time.time;
        carTarget = thrownCars[0].GetComponent<Transform>();

        //calculate player postion
        throwAtTarget = target.transform.position - transform.position;
        bossMethod = StartCoroutine(bossOne());

    }

    // Update is called once per frame
    void Update()
    {

        
        //spawnMoreMinions();
        /**bossActions();
        if (!actionTaken)
            actionTaken = true;

            if (bossAction == 0)
            {
                moveBoss();
            }

            if (bossAction == 1)
            {

                throwCarAtPlayer();//Random.Range(0,thrownCars.Length+1));
            }

            if (bossAction == 2)
            {
                boomarangShot();
            }*/
    }


    IEnumerator bossOne()
    {
        while (bossHealth > 0)
        {

            // pause and then move to location
            //yield return new WaitForSeconds(5);
            bossPoint = actionChoice();
            yield return new WaitForSeconds(2);

            while (transform.position.x != bossPoints[bossPoint].position.x)
            {
                Debug.Log("Walking to point: " + bossPoint);
                transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(bossPoints[bossPoint].transform.position.x, bossPoints[bossPoint].transform.position.y), speed);
                yield return null;
            }

            //pause then 3 fire projectiles
            yield return new WaitForSeconds(2);

            
            if (sawAction < 2)
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

            else if (sawAction >= 1 && bossPoint != 0)
            {
                //walk behind car
                while (transform.position.x != bossPoints[bossPoint + 4].position.x)
                {
                    Debug.Log("Walking to carPoint: " + bossPoint);
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint +4].transform.position.x, bossPoints[bossPoint+4 ].transform.position.y), speed);
                    yield return null;
                }

                yield return new WaitForSeconds(1);
                //Throw the car
                throwCarAtPlayer(bossPoint-1);

                // walk back from behind car
                while (transform.position.x != bossPoints[bossPoint + 4].position.x)
                {
                    Debug.Log("Walking back from carpoint to point: " + bossPoint);
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint].transform.position.x, bossPoints[bossPoint].transform.position.y), speed);
                    yield return null;
                }
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

    // fire boss saw blade
    Vector2 fireProjectile()
    {
        Vector2 trgt = target.transform.position - transform.position;
        projectile = Instantiate(projPrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(trgt.x, trgt.y);
        return trgt;

    }

    //return saw blade
    void returnProjectile(Vector2 vect)
    {
        //Vector2 trgt = transform.position - target.transform.position;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-vect.x, -vect.y);
    }


    // Random action for boss
    public  int actionChoice()
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
        int maximumSpawn = 3;
        int randomSpawnPoint = Random.Range(0, 2);

        Debug.Log("Generating Spawn at spawnPoint: " + randomSpawnPoint);


        if (Time.time > spawnMinion && minionCount < maximumSpawn)
        {
            Instantiate(minions, spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
            spawnMinion = Time.time + spawnRate;
            minionCount += 1;
        }
        else if(Time.time > spawnMinion && minionCount >= maximumSpawn)
        {
            Debug.Log("Maximum spawn = " + maximumSpawn);
        }
        else if (Time.time > spawnMinion)
        {
            Debug.Log("Error in spawnMoreMinions. Max Spawn:" + maximumSpawn);
        }
    }

    void bossActions()
    {
        
        //int randomdirection = Random.Range(0, 2);
        if (Time.time > actionTime)
        {
            bossAction = Random.Range(1, 3);
            actionTime = Time.time + 6.0f;
            
        }
        else
        {
            Debug.Log("bossActions waiting");
        }
    }

    void moveBoss()
    {
        Debug.Log("moveBoss Activated");

        transform.position = Vector2.MoveTowards(transform.position, carTarget.position, speed * Time.deltaTime);
    }

    void throwCarAtPlayer(int carNumber)
    {
        Debug.Log("Throwing car" + carNumber);
            //calculate player postion
            throwAtTarget = target.transform.position - transform.position;

            //apply force to car Object 
            Rigidbody2D carRB = thrownCars[carNumber].GetComponent<Rigidbody2D>();
            carRB.velocity = new Vector2(throwAtTarget.x, throwAtTarget.y);
            thrownCars[carNumber].GetComponent<ThrowCar>().thrown = true;

    }

    void boomarangShot()
    {
        Debug.Log("Boomarang shot");

        //calculate player postion
        throwAtTarget = target.transform.position - transform.position;

        //fire shot
        if (Time.time > shotTimer)
        {
            //generate projectile
            projectile = Instantiate(projPrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(throwAtTarget.x, throwAtTarget.y);

            //time between next shot
            shotTimer += 3.0f;
        }

        //call back shot
        if(Time.time > shotReturnTimer)
        {
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-throwAtTarget.x, -throwAtTarget.y);
            shotReturnTimer += 3.0f;
        }

        if(shotTimer == 12.0f)
        {
            actionTaken = false;
        }
    }

    void wait()
    {

        if(actionTime == 2.0f)
        {
            Debug.Log("waiting over");
            int randomAction = Random.Range(1, 3);
            bossAction = randomAction;
            actionTaken = true;
        }
        else
        {
            Debug.Log("Waiting " + actionTime);
        }
    }
}
