using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class startBossFight : MonoBehaviour
{

    private GameObject player;
    public GameObject boss;
    private BossFightOne2D start;
    public bool startFight;
    public Transform leftBarrierSpot;
    public Transform rightBarrierSpot;
    public GameObject leftBarrier;
    public GameObject rightBarrier;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player_stats>().healthCurrent <= 0)
        {
            startFight = false;
            removeBarriers();
            Debug.Log("Stop Boss fight");
            
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {

            Instantiate(leftBarrier, leftBarrierSpot.position, Quaternion.identity);
            Instantiate(rightBarrier, rightBarrierSpot.position, Quaternion.identity);
            startFight = true;
            Destroy(gameObject);
            Debug.Log("Destroying" + gameObject);

            
        }
    }

    public void removeBarriers()
    {

        Debug.Log("Bye Bye Barriers");
        Destroy(leftBarrier);
        Destroy(rightBarrier);
    }



}
