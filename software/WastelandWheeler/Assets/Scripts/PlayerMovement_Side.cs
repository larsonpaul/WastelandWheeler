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
    //GameObjects for animation
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer armSpriteRenderer;
    private Player_stats stats;
    private GameObject arm;
    private Vector3 mousePosition;
    private Camera cam;

    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        stats = gameObject.GetComponent<Player_stats>();
        arm = GameObject.Find("Arm");
        armSpriteRenderer = arm.GetComponent <SpriteRenderer>();
        cam = Camera.main;
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

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 look_direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(look_direction.y, look_direction.x) * Mathf.Rad2Deg;

        Vector3 armPosition = gameObject.transform.position;

        armPosition.y += 0.1f;

        //Flip sprite depending on mouse cursor position relative player.
        if (angle < 90.0f && angle > -90.0f)
        {
            spriteRenderer.flipX = false;
            armSpriteRenderer.flipX = false;
            armPosition.x -= 0.05f;
        }
        else if (angle > 90.0f || angle < -90.0f)
        {
            spriteRenderer.flipX = true;
            armSpriteRenderer.flipX = true;
            armPosition.x += 0.05f;
        }
        
        if (Input.GetKeyDown("space"))
        {
            Jumpcheck();
        }

        arm.transform.position = armPosition;  //Set the arm position to the player position
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");  //get player input
        rbody.velocity = new Vector2((h * stats.move_speed), rbody.velocity.y);



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
