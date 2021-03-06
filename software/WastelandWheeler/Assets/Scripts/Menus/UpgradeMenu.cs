﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    private int first_scene = 2;
    private int last_scene = 3; // this will need to be changed as more levels are added

    private const int ROAD = 2;
    private const int PCG = 3;

    private const int ARENA = 4;

    [SerializeField]
    private int arenaWeight = 30;

    private const int BOSS = 5;

    public Stat_Manager statManager;

    private void Start()
    {
        statManager = FindObjectOfType<Stat_Manager>();
    }

    public void StartNextLevel()
    {
        statManager.levelComplete();
        Debug.Log("Levels completed" + statManager.levelsComplete());

        // Time to battle the boss
        if (statManager.levelsComplete() % 3 == 0)
        {
            arenaWeight += 10;
            SceneManager.LoadScene(BOSS);
            PrepareScene();
            return;
        }

        int roll = Random.Range(1, 100);
        Debug.Log(roll);
        if (roll <= arenaWeight)
        {
            Debug.Log("Playing arena at weight " + arenaWeight);
            arenaWeight = 30;
            SceneManager.LoadScene(ARENA);
            PrepareScene();
            return;
        }
        else
        {
            arenaWeight += 10;
        }


        // default to road level
        SceneManager.LoadScene(PCG);
        PrepareScene();
        return;
    }

    private void PrepareScene()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
    }

}
