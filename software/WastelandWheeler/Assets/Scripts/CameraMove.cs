using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that moves the camera to the players location with every call to update.
 * Camera movement is done in Late Update to allow the player to move first.
 */
public class CameraMove : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,0,-5);

    private Transform player;

    // When game starts, find the player object
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // After update is called, move the position of the camera to the position of the player
    void LateUpdate()
    {
        if (player == null) return;
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); 
    }
}
