using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    // Added for shooting
    private Rigidbody2D r_body;
    Vector3 mouse_position;
    private Camera cam;
    GameObject player;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //r_body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       mouse_position = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector3 look_direction = mouse_position - transform.position;
        float angle = Mathf.Atan2(look_direction.y, look_direction.x) * Mathf.Rad2Deg -90.0f;
        Vector3 rotation = new Vector3(0.0f, 0.0f, angle);
        transform.eulerAngles = rotation;
        /*Vector2 look_direction = mouse_position - r_body.position;
        float angle = Mathf.Atan2(look_direction.y, look_direction.x) * Mathf.Rad2Deg - 90f;
        r_body.rotation = angle;
        */
    }

}

