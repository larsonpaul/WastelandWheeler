
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBehave : MonoBehaviour
{
    public float damage = 20f;
    private int lifetime = 100;
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

        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player_stats>().RemoveHealth(damage);
            
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
        }
    }
}
