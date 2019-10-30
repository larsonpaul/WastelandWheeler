using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float healthMax = 20f;
    public float healthCurrent = 20f;
    public float shields = 1;
    private GameObject dda;


    private void Start()
    {
        //dda = GameObject.Find("DDA");
        //dda.GetComponent<DynamicDifficultyAdjuster>().Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveHealth(float num)
    {
        if (num <= 0)
        {
            if (num < 0)
            {
                string error = gameObject.name + ".RemoveHealth() given negative float " + num;
                Debug.LogError(error);
            }
            return;
        }
        else
        {
            healthCurrent -= num;
            if (healthCurrent <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Function to grab the current health of the player
    public float GetHealth()
    {
        return healthCurrent;
    }
}


