using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedCamera_Side : MonoBehaviour
{
    
    public Vector3 offset = new Vector3(0, 0, -30);

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;
        if((System.Math.Abs(player.position.x - transform.position.x)) > Camera.main.orthographicSize * 0.25f)
        {
            if((player.position.x - transform.position.x) < 1)
            {
                offset.x = (Camera.main.orthographicSize * 0.25f);
                transform.position = new Vector3(player.position.x + offset.x, player.position.y, offset.z);
            }
            else
            {
                offset.x = -(Camera.main.orthographicSize * 0.25f);
                transform.position = new Vector3(player.position.x + offset.x, player.position.y , offset.z);
            }

        }
        else
        {
            transform.position = new Vector3(transform.position.x, player.position.y, offset.z);
        }

        
    }
}
