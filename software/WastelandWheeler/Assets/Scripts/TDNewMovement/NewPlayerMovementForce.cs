using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovementForce : MonoBehaviour
{

    private Rigidbody2D rbody;
    private Camera cam;
    private Player_stats stats;
    private SpriteRenderer renderer;

    private float speed;

    [SerializeField]
    private Sprite spr1, spr2, spr3, spr4, spr5, spr6, spr7, spr8;

    [SerializeField]
    private Vector2 movement;

    [SerializeField]
    private Vector2 facing;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rbody = GetComponent<Rigidbody2D>();
        stats = GetComponent<Player_stats>();
        renderer = GetComponent<SpriteRenderer>();

        facing = Vector2.right;
        angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement vector
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 1)
            movement.Normalize();

        // Calculate facing
        Vector2 sum = facing + movement;
        if (Vector2.Dot(facing, movement) == -1)
        {
            // vectors are directly opposite
            // pick an arbitrary direction to turn (right)
            movement = Vector2.Perpendicular(movement);
        }
        else if (Vector2.Dot(facing, movement) < -0.5)
        {
            // vectors are separated by an obtuse angle
            // find sign of angle, and turn the appropriate direction
            if (Vector2.SignedAngle(facing, movement) < 0)
            {
                movement = Vector2.Perpendicular(movement);
            }
            else
            {
                movement = -Vector2.Perpendicular(movement);
            }
        }

        float turnspeed = 0.2f + (0.6f / (rbody.velocity.magnitude + 1));

        facing = (facing + movement * turnspeed);

        if (facing.magnitude > 1)
            facing.Normalize();
        else if (facing.magnitude < 0.5f)
            facing = facing.normalized * 0.5f;


        // Calculate angle
        angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

        // Sprite facing
        if (angle < -22.5) angle += 360;

        switch ((int)((angle + 22.5) / 45) % 8)
        {
            case 0:
                renderer.sprite = spr3;
                break;
            case 1:
                renderer.sprite = spr4;
                break;
            case 2:
                renderer.sprite = spr5;
                break;
            case 3:
                renderer.sprite = spr6;
                break;
            case 4:
                renderer.sprite = spr7;
                break;
            case 5:
                renderer.sprite = spr8;
                break;
            case 6:
                renderer.sprite = spr1;
                break;
            case 7:
                renderer.sprite = spr2;
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        // Perform movement
        float x = facing.normalized.x * stats.move_speed * movement.magnitude;
        float y = facing.normalized.y * stats.move_speed * movement.magnitude;

        rbody.AddForce(new Vector2(x, y));
    }



}
