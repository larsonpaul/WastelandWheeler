using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    [SerializeField]
    private Player_stats stats;
    public float tick_down = 20.0f;
    public float tick_up = 5.0f;
    public float duration = .5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && stats.GetAdrenaline() >= tick_down )
        {
            gameObject.GetComponent<Player_stats>().RemoveAdrenaline(tick_down);
            Time.timeScale = .7f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Invoke("Disable", duration);
        }
    }

    private void Disable()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void EnemyAdrenaline()
    {
        gameObject.GetComponent<Player_stats>().AddAdrenaline(tick_up);
    }

}
