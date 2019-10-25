using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Side : MonoBehaviour
{

    public float fallspeed = 2.5f;
    public float hopMultiplier = 2f;
    public float jumpSpeed = 20f;
    public LayerMask Ground;
    public float groundDetect = 0.9f;
    private Rigidbody2D rbody; 
    public float velocity; // speed of velocity-based movement
    //GameObjects for animation
    private Animator animator; 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    // Makes the player jump if they are not on the ground
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

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h)); //Set direction for animation controller
        animator.SetBool("In Air", !IsGrounded()); //Set animation controller jump boolean

        //Flip sprite depending on current direction
        if (h < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (h > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");  //get player input

        rbody.velocity = new Vector2((h * velocity),rbody.velocity.y);

        if (Input.GetKeyDown("space"))
        {
            Jumpcheck();
        }

        if(rbody.velocity.y < 0)
        {
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (fallspeed - 1) * Time.deltaTime;
        }
        else if (rbody.velocity.y >0 && !Input.GetKey("space"))
        {
            rbody.velocity += Vector2.up * Physics2D.gravity.y * (hopMultiplier - 1) * Time.deltaTime;
        }
    }
}
