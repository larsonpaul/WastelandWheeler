using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackSideMove : MonoBehaviour
{
    private Rigidbody2D rbody;
    public float fallspeed = 2.5f;
    public float hopMultiplier = 2f;
    public float jumpSpeed = 20f;
    public float velocity;
    public float thrust;

    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //get player input
        float h = Input.GetAxis("Horizontal");
        rbody.velocity = new Vector2((h * velocity), rbody.velocity.y);


        if (Input.GetKey("space"))
        {
            Vector3 force = new Vector3(0, thrust, 0);
            rbody.AddForce(force, ForceMode2D.Force);
        }

        if (rbody.velocity.y < 0)
        {
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (fallspeed - 1) * Time.deltaTime;
        }
        else if (rbody.velocity.y > 0 && !Input.GetKey("space"))
        {
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (hopMultiplier - 1) * Time.deltaTime;
        }
    }
}