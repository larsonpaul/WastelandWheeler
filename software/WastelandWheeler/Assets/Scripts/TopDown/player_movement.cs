using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D r_body;
    private Camera cam;

    private static Player_stats stats;
    private float move_speed;
    public Sprite img1, img2, img3, img4, img5, img6, img7, img8;

    Vector2 movement;
    Vector2 mouse_position;

    // Start is called before the first frame update
    void Start()
    {
        r_body = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    // Update is called once per frame
    void Update()
    {
        move_speed = stats.move_speed;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        mouse_position = cam.ScreenToWorldPoint(Input.mousePosition);
        
        
    }

    private void FixedUpdate()
    {
        r_body.MovePosition(r_body.position + movement * move_speed * Time.fixedDeltaTime);

        Vector2 look_direction = mouse_position - r_body.position;
        float angle = Mathf.Atan2(look_direction.y, look_direction.x) * Mathf.Rad2Deg - 90f;

        r_body.rotation= angle;

        if (r_body.rotation > -20 && r_body.rotation < 20)
        {
            this.GetComponent<SpriteRenderer>().sprite = img5;
            this.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (r_body.rotation > 20 && r_body.rotation < 65)
        {
            this.GetComponent<SpriteRenderer>().sprite = img6;
            this.GetComponent<SpriteRenderer>().flipY = false;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (r_body.rotation < 90 && r_body.rotation > 65)
        {
            this.GetComponent<SpriteRenderer>().sprite = img7;
            this.GetComponent<SpriteRenderer>().flipY = true;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (r_body.rotation < -250 && r_body.rotation > -270)
        {
            this.GetComponent<SpriteRenderer>().sprite = img7;
            this.GetComponent<SpriteRenderer>().flipY = true;
            this.GetComponent<SpriteRenderer>().flipX = true;

        }

        if (r_body.rotation < -205 && r_body.rotation > -250)
        {
            this.GetComponent<SpriteRenderer>().sprite = img8;
        }

        if (r_body.rotation < -160 && r_body.rotation > -205)
        {
            this.GetComponent<SpriteRenderer>().sprite = img1;
            this.GetComponent<SpriteRenderer>().flipY = true;
        }

        if (r_body.rotation < -110 && r_body.rotation > -160)
        {
            this.GetComponent<SpriteRenderer>().sprite = img2;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (r_body.rotation < -65 && r_body.rotation > -110)
        {
            this.GetComponent<SpriteRenderer>().sprite = img3;
            this.GetComponent<SpriteRenderer>().flipY = true;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (r_body.rotation < -20 && r_body.rotation > -65)
        {
            this.GetComponent<SpriteRenderer>().sprite = img4;
            this.GetComponent<SpriteRenderer>().flipY = false;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }


    }
}
