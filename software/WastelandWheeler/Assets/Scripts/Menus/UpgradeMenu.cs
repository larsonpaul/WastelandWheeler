using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    private int first_scene = 2;
    private int last_scene = 2; // this will need to be changed as more levels are added

    private const int ROAD = 2;

    private const int ARENA = 3;
    private int arenaWeight = 20;


    public void StartNextLevel()
    {
        int roll = Random.Range(1, 100);
        Debug.Log(roll);
        if (roll <= arenaWeight)
        {
            SceneManager.LoadScene(ARENA);
            PrepareScene();
            return;
        }
        roll -= arenaWeight;

        // default to road level
        SceneManager.LoadScene(ROAD);
        PrepareScene();
        return;
    }

    private void PrepareScene()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
    }
}
