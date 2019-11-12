﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    private int first_scene = 2;
    private int last_scene = 3; // this will need to be changed as more levels are added

    public void StartNextLevel()
    {
        int chosen_scene = Random.Range(first_scene, last_scene + 1);
        SceneManager.LoadScene(chosen_scene);
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
    }
}
