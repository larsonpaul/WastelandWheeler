﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D r_body;
    private Camera cam;

    private static Player_stats stats;
    private float move_speed;

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

    }
}
