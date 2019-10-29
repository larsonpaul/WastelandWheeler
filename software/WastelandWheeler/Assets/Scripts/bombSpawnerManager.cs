using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombSpawnerManager : MonoBehaviour
{

    public float spawnRate = 0.3f;
    public GameObject bombPrefab;
    private float nextTimeToSpawn = 0.0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            nextTimeToSpawn = Time.time + 1f / spawnRate;
        }
    }
}
