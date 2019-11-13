using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLayerBehaviour : MonoBehaviour
{
    public float shotStartTime;
    private float rate_of_fire;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rate_of_fire = shotStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(rate_of_fire <= 0)
        {
            GameObject projectile = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            rate_of_fire = shotStartTime;
        }
        else
        {
            rate_of_fire -= Time.deltaTime;
        }
    }
}
