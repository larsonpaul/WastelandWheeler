using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameMove : MonoBehaviour
{
    

    
    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(transform.position.x+ 0.05f, transform.position.y, transform.position.z);
    }
}
