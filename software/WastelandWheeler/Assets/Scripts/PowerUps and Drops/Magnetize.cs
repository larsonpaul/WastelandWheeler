using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetize : MonoBehaviour
{
    Rigidbody2D rbody;
    GameObject player;
    Vector2 playerDirection;
    bool flyToPlayer;

    [SerializeField]
    private float velocity = 15f;

    [SerializeField]
    private int maxFrames = 60;
    private int curFrames = 30;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        curFrames = maxFrames;
    }

    private void FixedUpdate()
    {
        if (flyToPlayer && curFrames > 0)
        {
            Vector2 deltaMovement = (player.transform.position - transform.position) / (curFrames--);
            transform.Translate(deltaMovement);

            //playerDirection = -(transform.position - player.transform.position).normalized;
            //rbody.velocity = new Vector2(playerDirection.x, playerDirection.y) * 15f * (Time.time / timeStamp);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("DropMagnet"))
        {
            player = GameObject.Find("Player");
            flyToPlayer = true;
        }
    }
}