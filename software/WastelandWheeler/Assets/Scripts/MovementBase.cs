using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{

    public enum MOVEMENT_TYPE { UNIT, FORCE, VELOCITY };

    public float unit_speed;
    public float force_speed;
    public float velocity;

    public MOVEMENT_TYPE movement_type;

    private Rigidbody2D rbody;

    private Vector2 dir;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = getDirection();

        switch (movement_type)
        {
            case MOVEMENT_TYPE.UNIT:
                // direct movement
                Vector2 pos = transform.position;
                transform.position = new Vector2(pos.x + unit_speed * dir.x, pos.y + unit_speed * dir.y);
                break;
            case MOVEMENT_TYPE.FORCE:
                // physical simulation
                rbody.AddForce(new Vector2(force_speed * dir.x, force_speed * dir.y));
                break;
            case MOVEMENT_TYPE.VELOCITY:
                // override velocity directly
                rbody.velocity = new Vector2(velocity * dir.x, velocity * dir.y);
                break;
        }
    }

    public virtual Vector2 getDirection()
    {
        return new Vector2(1, 1);
    }

}
