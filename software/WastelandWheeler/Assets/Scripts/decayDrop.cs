using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decayDrop : MonoBehaviour
{
    private int lifetime = 250;
    // Update is called once per frame
    void Update()
    {
        if (lifetime == 0)
        {
            Destroy(gameObject);
        }
        lifetime--;
    }
}
