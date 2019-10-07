﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement_top : MonoBehaviour
{
    public float move_speed = 2f;
    public Rigidbody2D r_body;
    public Camera cam;
    Vector2 movement;
    Vector2 mouse_position;
    public Sprite img1, img2, img3, img4, img5, img6, img7, img8;

    // Update is called once per frame
    void Update()
    {
        // Get the x and y axis
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Get mouse position from on-screen
        mouse_position = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    // Fixed update to move character
    private void FixedUpdate()
    {
        // Move the Rigidbody connected to the gameObject based upon current position, input key and move speed. 
        r_body.MovePosition(r_body.position + movement * move_speed * Time.fixedDeltaTime);

        // Create a temp variable for the look direction
        Vector2 look_direction = mouse_position - r_body.position;

        // Do some maths to find the angle that we will shoot in 
        float angle = Mathf.Atan2(look_direction.y, look_direction.x) * Mathf.Rad2Deg - 90f;

        // Set rotation to calculated angle
        r_body.rotation = angle;
     
        if (r_body.rotation > -70 && r_body.rotation < 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = img5;
            this.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (r_body.rotation < -70 && r_body.rotation > -110)
        {
            this.GetComponent<SpriteRenderer>().sprite = img3;
            this.GetComponent<SpriteRenderer>().flipY = true;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (r_body.rotation < -110  && r_body.rotation > -160)
        {
            this.GetComponent<SpriteRenderer>().sprite = img2;
            //this.GetComponent<SpriteRenderer>().flipY = true;
            //this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (r_body.rotation < -160 && r_body.rotation > -200)
        {
            this.GetComponent<SpriteRenderer>().sprite = img1;
            this.GetComponent<SpriteRenderer>().flipY = true;
        }

        if (r_body.rotation < -200 && r_body.rotation > -250)
        {
            this.GetComponent<SpriteRenderer>().sprite = img8;
            //this.GetComponent<SpriteRenderer>().flipY = true;
            //this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (r_body.rotation < -250 && r_body.rotation > -270)
        {
            this.GetComponent<SpriteRenderer>().sprite = img7;
            this.GetComponent<SpriteRenderer>().flipY = true;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

    }
   
}
