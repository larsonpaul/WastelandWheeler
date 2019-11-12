using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float damage = 10f;
    private int lifetime = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        if (lifetime == 0)
        {
            Destroy(gameObject);
        }
        lifetime--;
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),col);
        }
        else if (col.CompareTag("Shot"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),col);
        }
        else if (col.CompareTag("Power_Up"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),col);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
