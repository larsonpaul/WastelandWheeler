using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //public GameObject hit_effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject effect = Instantiate(hit_effect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            return;
        }
        if (obj.CompareTag("Shot"))
        {
            return;
        }
        if (obj.CompareTag("Power_Up"))
        {
            return;
        }
        Destroy(gameObject);
    }
}
