using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/** To use: Create 7 bossPoint and drag them into the array.  Also needs projectiles, firepoint, UI slide and target.
 *  Create the play area using four platforms arrange in a clockwise formation. Move the bossPoints to the positions as shown
 *             -6-
 *        5-4        2-3
 *         ----0-1-----
 *
 *   Add an invisible object that locates BossFight Script and changes boll startRoutine to true then Destroys itself 
 */


public class BossFightOne2D : MonoBehaviour
{

    public Transform[] bossPoints;  //points where boss will move to
    public float speed; // boss's speed
    private int bossPoint; // current point form the array of bossPoints
    private GameObject player;

    private Rigidbody2D rb;
    public GameObject target; // target/player for boss's aim


    public Slider bossHealthBar;
    public float bossHealth = 100f;
    public Transform firePoint; // firePoint for projectiles

    public GameObject[] barriers; // Create objects with colliders and store in array. Prevents player from leaving area
    private GameObject projectile;  // Boss's projectiles
    public GameObject projPrefab;
    public startBossFight startRoutine;       // starts the script once player reaches a certain spot 
    Coroutine bossMethod;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startRoutine = FindObjectOfType<startBossFight>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (startRoutine.startFight)
        {

            bossMethod = StartCoroutine(bossOne());
            startRoutine.startFight = false;
        }

        if (player.GetComponent<Player_stats>().healthCurrent <= 0)
        {
            Debug.Log("We have stats!!!!!");
            stopBossFight();
        }


        // Update Boss's Health
        //bossHealthBar.value = bossHealth;
        if (bossHealth <= 0f)
        {
            Debug.Log("Barries Destroyed!!!");
            startRoutine.startFight = false;
            StopCoroutine(bossOne());
            startRoutine.removeBarriers();
        }
    }


    IEnumerator bossOne()
    {

        // go to point one or two 
        bossPoint = randomNumber(0, 2);
        while (bossHealth > 0)
        {
            if (bossPoint == 0 || bossPoint == 1)
            {

                while (transform.position.x != bossPoints[bossPoint].position.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint].transform.position.x, transform.position.y), speed);
                    yield return null;
                }

                jump(8);
                yield return new WaitForSeconds(1);
                //Shoot three projectiles

                int i = 3;
                while (i > 0)
                {
                    Vector2 vect = fireProjectile();
                    yield return new WaitForSeconds(1);
                    returnProjectile(vect);
                    yield return new WaitForSeconds(1);
                    i--;
                }

                yield return new WaitForSeconds(1);

                // jump to level 1
                bossPoint = jump(bossPoint);

                //go to the end of platform
                yield return new WaitForSeconds(3);
                //Debug.Log("Choice: " + bossPoint);
                while (transform.position.x != bossPoints[bossPoint + 1].position.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint + 1].transform.position.x, transform.position.y), speed);
                    yield return null;
                }

                // Decide what type of fire to use
                int boomarangOrburst = randomNumber(0, 2);

                // boomarang shot
                if (boomarangOrburst == 0)
                {

                    jump(8);
                    yield return new WaitForSeconds(1);

                    //Shoot three projectiles
                    i = 3;
                    while (i > 0)
                    {
                        Vector2 vect = fireProjectile();
                        yield return new WaitForSeconds(1);
                        returnProjectile(vect);
                        i--;

                    }
                }


                //burst shoot
                else
                {
                    jump(8);
                    yield return new WaitForSeconds(1);
                    jump(8);
                    yield return new WaitForSeconds(1);

                    i = 4;
                    while (i > 0)
                    {
                        Vector2 trgt = target.transform.position - transform.position;
                        projectile = Instantiate(projectile, firePoint.position, Quaternion.identity);
                        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(trgt.x, trgt.y),
                        ForceMode2D.Impulse);

                        yield return new WaitForSeconds(1);
                        i--;
                    }

                }

                //go back to the other end of platform
                yield return new WaitForSeconds(3);
                //Debug.Log("Going back" + bossPoint);
                while (transform.position.x != bossPoints[bossPoint].position.x)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(bossPoints[bossPoint].transform.position.x, transform.position.y), speed);
                    yield return null;
                }

                    bossPoint = randomNumber(0, 2);

            }
            else
            {

                //Debug.Log("Instead " + bossPoint);
                bossPoint = jump(bossPoint);
                bossPoint = randomNumber(0, 2);
                yield return new WaitForSeconds(3);
            }

        }

        yield return null;
    }


    // jump on platform
    private int jump(int jumpPoint)
    {
        if (jumpPoint == 8)
        {
            //Debug.Log("Jumping up " + jumpPoint);
            rb.AddForce(Vector2.up * 100);

        }
        // jump left
        else if (jumpPoint == 0 || jumpPoint == 2)
        {
            //Debug.Log("Jumping left " + jumpPoint);
            rb.AddForce(Vector2.up * 430); // magic numbers for current scene
            rb.AddForce(Vector2.left * 90);
        }
        else // jump right 
        {
            //Debug.Log("Jumping right " + jumpPoint);
            rb.AddForce(Vector2.up * 430); // magic numbers for current scene
            rb.AddForce(Vector2.right * 90);
        }
        if (jumpPoint == 0)
        {
            return 2;
        }
        else
        {
            return 4;
        }

    }

    // generates randon integer for actions and move options
    int randomNumber(int minRange, int maxRange)
    {
        int randomMove = Random.Range(minRange, maxRange);
        return randomMove;
    }

    Vector2 fireProjectile()
    {
        Vector2 trgt = target.transform.position - transform.position;
        projectile = Instantiate(projPrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(trgt.x, trgt.y);
        return trgt;

    }

    void returnProjectile(Vector2 vect)
    {
        //Vector2 trgt = transform.position - target.transform.position;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-vect.x, -vect.y);
    }

    public void stopBossFight()
    {
        Debug.Log("STOP STOP STOP");
        startRoutine.startFight = false;
        startRoutine.removeBarriers();
        StopCoroutine(bossMethod);
    }

}

