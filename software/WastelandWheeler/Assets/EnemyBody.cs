/**
 *  Make sure that the enemy has Rigid body 2D, Enemy body and Collision 2D.
 *  For rigid body:Choose Kinimatic Body Type
 *  Layer: Add them to the enemy layer
 *  For Enemy Body script, Choose Speed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public LayerMask enemyMask;
    public float mySpeed;
    Rigidbody2D myBody;
    Transform myTransform;
    float myWidth;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lineCastPos = myTransform.position - myTransform.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        if (!isGrounded)
        {
            Vector3 currRot = myTransform.eulerAngles;
            currRot.y += 180;
            myTransform.eulerAngles = currRot;
        }

        Vector2 myVelocity = myBody.velocity;
        myVelocity.x = -myTransform.right.x * mySpeed;
        myBody.velocity = myVelocity;
    }

}