using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class startBossFight : MonoBehaviour
{

    private GameObject player;
    public GameObject boss;
    private BossFightOne2D start;
    public bool startFight;
    public GameObject stopBossFight;
    public GameObject leftBarrier;
    public GameObject rightBarrier;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //start battle and activate stop battle object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {

            leftBarrier.SetActive(true);
            rightBarrier.SetActive(true);
            startFight = true;
            Debug.Log("Destroying" + gameObject);
            stopBossFight.SetActive(true);
            stopBossFight.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.SetActive(false);
        }
    }

    public void removeBarriers()
    {
        Debug.Log("Bye Bye Barriers");
    }
}
