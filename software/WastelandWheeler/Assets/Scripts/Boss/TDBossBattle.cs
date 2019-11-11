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

    private GameObject projectile;  // Boss's projectiles
    public GameObject projPrefab; 
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

        spawnMoreMinions();

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
                thrownCars[bossPoint -1].GetComponent<ThrowCar>().thrown == false)
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
                //Throw the car aftermaking it dynamic
                thrownCars[bossPoint - 1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                throwCarAtPlayer(bossPoint-1);

                // walk back from behind car
                while (transform.position.x != bossPoints[bossPoint + 4].position.x)
                {
                    Debug.Log("Walking back from carpoint to point: " + bossPoint);
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint].transform.position.x, bossPoints[bossPoint].transform.position.y), speed);
                    yield return null;
                }

                thrownCars[bossPoint - 1].GetComponent<ThrowCar>().collision = true;
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
}
