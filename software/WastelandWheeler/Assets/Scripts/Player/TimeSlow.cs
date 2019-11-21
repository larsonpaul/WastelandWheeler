using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    private Player_stats stats;
    private float cost = 25.0f;
    private float slowdownFactor = 0.7f;
    private float slowdownLength = 0.75f;

    private const float DELTA = 0.02f;

    private bool isSlowed = false;

    void Start()
    {
        stats = gameObject.GetComponent<Player_stats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && stats.GetAdrenaline() >= cost && !isSlowed)
        {
            isSlowed = true;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = DELTA * Time.timeScale;
            stats.RemoveAdrenaline(cost);
            Invoke("Disable", slowdownLength);
        }
    }

    private void Disable()
    {
        isSlowed = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = DELTA * Time.timeScale;
    }

}
