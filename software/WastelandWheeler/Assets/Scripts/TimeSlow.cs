using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    private static Player_stats stats;
    public float tick_down = 20.0f;
    public float duration = .5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Time.timeScale = .5f;
            gameObject.GetComponent<Player_stats>().RemoveAdrenaline(tick_down);
            Invoke("Disable", duration);
        }
    }

    private void Disable()
    {
        Time.timeScale = 1f;
    }

}
