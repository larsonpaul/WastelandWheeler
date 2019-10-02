using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,0,-5);

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); 
    }
}
