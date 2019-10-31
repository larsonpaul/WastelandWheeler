using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopBossFight : MonoBehaviour
{

    private GameObject player;
    public GameObject boss;
    private BossFightOne2D stop;
    public bool startFight;
    public GameObject startBossFight;
    public GameObject leftBarrier;
    public GameObject rightBarrier;


    // Start is called before the first frame update
    void Start()
    {
        stop = FindObjectOfType<BossFightOne2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //stop the battle and remove barriers when player dies
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {

            leftBarrier.SetActive(false);
            rightBarrier.SetActive(false);
            //Destroy(gameObject);
            Debug.Log("Stop the battle");

            // activate startbattle object
            startBossFight.SetActive(true);
            startBossFight.GetComponent<BoxCollider2D>().isTrigger = true;
            stop.stopBossFight();

        }
    }

}
