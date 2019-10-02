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


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click!");
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Click!");
            GameObject enemy = Instantiate(prefab, Camera.main.ViewportToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
    }

}
