using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    private int first_scene;
    private int last_scene;
    public Player_stats stats;

    private void Start()
    {
        first_scene = 2;
        last_scene = first_scene; // this will need to be changed as more scenes are added
        stats = GameObject.Find("Player").GetComponent<Player_stats>();
    }

    public void StartNextLevel()
    {
        int chosen_scene = Random.Range(first_scene, last_scene + 1);
        SceneManager.LoadScene(chosen_scene);
        stats.Respawn();
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
    }
}
