using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D playerRB;

    public GameObject bulletPrefab;
    public float bullet_force = 20f;
    private float timer = 0f;
    private Player_stats stats;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerRB = GetComponent<Rigidbody2D>();
        stats = GetComponent<Player_stats>();
    }

    // Update is called once per frame
    void Update()
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
            Shoot(angle);
            timer = stats.rate_of_fire;
        }

    }

    private void Shoot(float angle)
    {
        GameObject fp = new GameObject();
        Vector2 origin = Vector2.up;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        origin = rotation * (origin * 0.5f);
        origin = origin + playerRB.position;
        fp.transform.Translate((Vector2)gameObject.transform.position + origin);
        fp.transform.Rotate(rotation.eulerAngles);
        Vector2 leftOffset = transform.position;
        leftOffset.x += -.7f;
        Vector2 rightOffset = transform.position;
        rightOffset.x += .7f;

        GameObject bullet = Instantiate(bulletPrefab, origin, rotation);
        bullet.GetComponent<Bullet>().SetDamage(stats.GetDamage());
        bullet.GetComponent<Bullet>().SetSize(stats.GetBulletSize());

        // TripleShot powerup functionality
        if (stats.tripleShot)
        {
            Vector2 bulletForce = (Vector2)(fp.transform.up * bullet_force) + playerRB.velocity / 2;

            GameObject bulletLeft = Instantiate(bulletPrefab, leftOffset, rotation);
            GameObject bulletRight = Instantiate(bulletPrefab, rightOffset, rotation);
            
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRBLeft = bulletLeft.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRBRight = bulletRight.GetComponent<Rigidbody2D>();

            bulletRB.AddForce(bulletForce, ForceMode2D.Impulse);
            bulletRBLeft.AddForce(bulletForce, ForceMode2D.Impulse);
            bulletRBRight.AddForce(bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            Vector2 bulletForce = (Vector2)(fp.transform.up * bullet_force) + playerRB.velocity / 2;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(bulletForce, ForceMode2D.Impulse);
        }

        Destroy(fp);
    }
}
