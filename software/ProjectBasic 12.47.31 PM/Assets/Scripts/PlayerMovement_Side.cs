using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Side : MonoBehaviour
{

    public float fallspeed = 1.5f;
    public float hopMultiplier = 2.5f;
    public float jumpSpeed = 20f;
    public LayerMask Ground;
    public float groundDetect = 0.9f;
    private Rigidbody2D rbody; 
    public float velocity; // speed of velocity-based movement
    
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>(); 
    }



    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;


        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundDetect, Ground);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }


    void Jumpcheck()
    {
        if (!IsGrounded())
        {
            return;
        }
        else
        {
            rbody.velocity = new Vector2(rbody.velocity.x, jumpSpeed);
        }



    }

    void Update()
    {
        //get player input
        float h = Input.GetAxis("Horizontal");
        rbody.velocity = new Vector2((h * velocity),rbody.velocity.y);

        if (Input.GetKeyDown("space"))
        {
            Jumpcheck();
        }
    }

}
