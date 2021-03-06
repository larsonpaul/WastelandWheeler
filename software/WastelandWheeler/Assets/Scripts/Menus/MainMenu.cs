﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * Class that runs the MainMenu scene by loading the other game scenes
 */
public class MainMenu : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject TutorialPanel;
    public GameObject PowerupsPanel;

    private Stat_Manager stats;

    void Start()
    {
        stats = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
    }

    // Loads scene one in the build
    public void StartTutorial()
    {
        MainCanvas.SetActive(false);
        TutorialPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }
    }

    public void ContinueTutorial()
    {
        TutorialPanel.SetActive(false);
        PowerupsPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        stats.Reset();

        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        PowerupsPanel.SetActive(false);
    }

    // Stop application 
    public void QuitGame()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }
}
