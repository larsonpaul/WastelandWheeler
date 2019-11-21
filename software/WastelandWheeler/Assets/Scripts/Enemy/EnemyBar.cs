using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBar : MonoBehaviour
{
    private Transform healthBar;
    private Transform scaleBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("HealthBar");
        scaleBar = healthBar.Find("Bar");
    }
    
    // scale to the enemy
    public void SetScale(float x)
    {
        if (x < 1) healthBar.gameObject.SetActive(true);

        Vector3 scale = new Vector3(Mathf.Min(Mathf.Max(x, 0), 1), 1f);

        scaleBar.localScale = scale;
    }
}
