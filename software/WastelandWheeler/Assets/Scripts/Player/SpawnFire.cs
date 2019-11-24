using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFire : MonoBehaviour
{
    public float shotStartTime;
    private float rate_of_fire;
    public GameObject fire;
    public Player_stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Player_stats>();
        rate_of_fire = shotStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.onFire)
        {
            //check if you should lay fire 
            if (rate_of_fire <= 0)
            {
                GameObject projectile = (GameObject)Instantiate(fire, transform.position, Quaternion.identity);
                rate_of_fire = shotStartTime;
            }
            else
            {
                rate_of_fire -= Time.deltaTime;
            }
        }
    }
}
