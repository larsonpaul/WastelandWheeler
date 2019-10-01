using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //public GameObject hit_effect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject effect = Instantiate(hit_effect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
