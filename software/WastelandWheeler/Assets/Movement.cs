﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum MOVEMENT_TYPE { UNIT, FORCE, VELOCITY };

    public float unit_speed;
    public float force_speed;
    public float velocity;

    public MOVEMENT_TYPE movement_type;

    private Rigidbody2D rbody;

    public float angle;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        cam = Camera.main;

    }

    public float mouseX, mouseY, mouseZ;

    // Update is called once per frame
    void Update()
    {
        // gameObject; // shortcut to get current GameObject

        // Get User Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerpos = rbody.position;

        mouseX = mousepos.x;
        mouseY = mousepos.y;
        mouseZ = mousepos.z;

        //rbody.MoveRotation(angle);
        //rbody.rotation = angle;

        switch (movement_type)
        {
            case MOVEMENT_TYPE.UNIT:
                // direct movement
                Vector2 pos = transform.position;
                transform.position = new Vector2(pos.x + unit_speed * h, pos.y + unit_speed * v);
                break;
            case MOVEMENT_TYPE.FORCE:
                // physical simulation
                rbody.AddForce(new Vector2(force_speed * h, force_speed * v));
                break;
            case MOVEMENT_TYPE.VELOCITY:
                // override velocity directly
                rbody.velocity = new Vector2(velocity * h, velocity * v);
                break;
        }
    }


}
