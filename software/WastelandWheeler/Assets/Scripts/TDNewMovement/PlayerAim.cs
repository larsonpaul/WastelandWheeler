using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rbody;

    public GameObject bulletPrefab;
    public float bullet_force = 20f;
    private float timer = 0f;
    private Player_stats stats;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rbody = GetComponent<Rigidbody2D>();
        stats = GetComponent<Player_stats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction
        Vector2 mpos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 target = mpos - rbody.position;
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
        origin = origin + rbody.position;
        fp.transform.Translate((Vector2)gameObject.transform.position + origin);
        fp.transform.Rotate(rotation.eulerAngles);

        GameObject bullet = Instantiate(bulletPrefab, origin, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(fp.transform.up * bullet_force, ForceMode2D.Impulse);
        Destroy(fp);
    }
}
