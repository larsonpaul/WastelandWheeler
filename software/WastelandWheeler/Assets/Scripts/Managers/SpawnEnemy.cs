using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Input.mousePosition; 
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0f;
            GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
