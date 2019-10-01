using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float move_speed = 5f;

    public Rigidbody2D r_body;

    public Camera cam;

    Vector2 movement;
    Vector2 mouse_position;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
