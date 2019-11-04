using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombBehave : MonoBehaviour
{
    public GameObject splodePrefab;
    private int lifetime = 250;
    private Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        float angle = Random.Range(-100f, 100f);
        rbody.AddForce(new Vector2(angle, 400f));
    }

    void Update()
    {
        if (lifetime == 0)
        {
            Instantiate(splodePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        lifetime--;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Instantiate(splodePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
