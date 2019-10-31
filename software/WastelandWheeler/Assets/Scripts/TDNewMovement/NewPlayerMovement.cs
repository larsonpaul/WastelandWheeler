using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
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
        Debug.Log(angle);

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
        if (Vector2.Dot(facing, movement) < -0.999f)
        {
            movement = Vector2.Perpendicular(movement);
        }

        float turnspeed = 0.1f;
        facing = (facing + movement * turnspeed);

        if (facing.magnitude > 1)
            facing.Normalize();
        else if (facing.magnitude < 0.7f)
            facing = facing.normalized * 0.7f;


        // Calculate angle
        angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;

        // Sprite facing
        if (angle < -22.5) angle += 360;

        switch((int)((angle + 22.5)/45))
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
            case 8:
                renderer.sprite = spr3;
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        // Perform movement
        rbody.MovePosition(rbody.position + facing.normalized * stats.move_speed * movement.magnitude * Time.fixedDeltaTime);
    }



}
