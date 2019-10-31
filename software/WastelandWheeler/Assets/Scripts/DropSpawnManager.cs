using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawnManager : MonoBehaviour
{

    public float spawnRate = 1.0f;
    public GameObject ballPrefab;
    private float nextTimeToSpawn = 0.0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            Instantiate(ballPrefab, transform.position, Quaternion.identity);
            nextTimeToSpawn = Time.time + 1f / spawnRate;
        }
    }
}
