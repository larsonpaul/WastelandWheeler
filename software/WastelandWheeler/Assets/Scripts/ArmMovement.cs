using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    // Added for shooting
    private Rigidbody2D r_body;
    Vector2 mouse_position;
    private Camera cam;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");     //Get player object so we can access the transform component.
    }
    // Start is called before the first frame update
    void Start()
    {
        r_body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mouse_position = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        gameObject.transform.position = player.transform.position;  //Set the arm position to the player position

        Vector2 look_direction = mouse_position - r_body.position;
        float angle = Mathf.Atan2(look_direction.y, look_direction.x) * Mathf.Rad2Deg - 90f;
        r_body.rotation = angle;
    }

}

