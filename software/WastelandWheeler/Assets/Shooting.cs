using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform fire_point;
    public GameObject bulletPreFab;
    public float bullet_force = 20f;
    private float fire_timer = 0f;

    private static Player_stats stats;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fire_timer > 0)
        {
            fire_timer = Mathf.Max(0, fire_timer - Time.deltaTime);
        }

        if (Input.GetButton("Fire1") && fire_timer <= 0)
        {
            Shoot();
            //ROF_up fast_rof = gameObject.GetComponent<ROF_up>();
            //fire_timer = fast_rof.new_rof;
            fire_timer = stats.rate_of_fire;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPreFab, fire_point.position, fire_point.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(fire_point.up * bullet_force, ForceMode2D.Impulse);
    }
}
