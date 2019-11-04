using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFire : MonoBehaviour
{
    public float mySpeed;
    Rigidbody2D myBody;
    Transform myTransform;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 myVelocity = myBody.velocity;
        myVelocity.x = myTransform.right.x * mySpeed;
        myBody.velocity = myVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(GameObject.FindWithTag("bullet"));
    }


}
