using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    Rigidbody2D rBody;
    GameObject player;
    Vector2 playerDirection;
    float timeStamp;
    bool flyToPlayer;

    int value = 1;

    private bool used = false;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (flyToPlayer)
        {
            playerDirection = -(transform.position - player.transform.position).normalized;
            rBody.velocity = new Vector2(playerDirection.x, playerDirection.y) * 10f * (Time.time / timeStamp);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("DropMagnet"))
        {
            timeStamp = Time.time;
            player = GameObject.Find("Player");
            flyToPlayer = true;
        }
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            col.GetComponent<Player_stats>().playCoin = true;
            col.GetComponent<Player_stats>().totalCoins += value;
            Destroy(gameObject);
        }
    }
}
