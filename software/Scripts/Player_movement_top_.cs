﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement_top_ : MonoBehaviour
{
    public float move_speed = 2f;

    public Rigidbody2D r_body;

    public Camera cam;

    Vector2 movement;
    Vector2 mouse_position;

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
        r_body.rotation= angle;
    }
}
