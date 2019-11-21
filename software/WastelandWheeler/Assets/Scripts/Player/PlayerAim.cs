using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D playerRB;

    public GameObject bulletPrefab;
    public float bullet_size;
    public float bullet_force = 20f;
    private float timer = 0f;
    private Player_stats stats;
    private AudioSource blaster;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerRB = GetComponent<Rigidbody2D>();
        stats = GetComponent<Player_stats>();
        bullet_size = stats.bullet_size;
        bulletPrefab.GetComponent<Transform>().localScale = Vector3.one * bullet_size;
        blaster = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // Calculate direction
        Vector2 mpos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 target = mpos - playerRB.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;

        if (timer > 0)
        {
            timer = Mathf.Max(0, timer - Time.deltaTime);
        }
        else if (Input.GetButton("Fire1"))
        {
            blaster.Play();
            Shoot(angle);
            timer = stats.rate_of_fire;
        }

    }

    private void Shoot(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, playerRB.position, Quaternion.AngleAxis(angle, Vector3.forward));
        bullet.transform.Translate(Vector3.up * 0.5f);

        bullet.GetComponent<Bullet>().SetDamage(stats.GetDamage());
        bullet.GetComponent<Bullet>().SetSize(stats.GetBulletSize());

        Vector2 bulletForce = (Vector2)(bullet.transform.up * bullet_force) + playerRB.velocity / 2;

        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bulletForce, ForceMode2D.Impulse);

        // TripleShot powerup functionality
        if (stats.tripleShot)
        {
            GameObject bulletLeft = Instantiate(bullet);
            GameObject bulletRight = Instantiate(bullet);

            bulletLeft.transform.Translate(Vector3.left * 0.7f);
            bulletRight.transform.Translate(Vector3.right * 0.7f);

            Rigidbody2D bulletRBLeft = bulletLeft.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRBRight = bulletRight.GetComponent<Rigidbody2D>();

            bulletRBLeft.AddForce(bulletForce, ForceMode2D.Impulse);
            bulletRBRight.AddForce(bulletForce, ForceMode2D.Impulse);
        }

        //Destroy(fp);
    }
}
