﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance
    //{
    //    get;
    //    set;
    //}

    //void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}

    public float endDelay = 3f;

    [SerializeField]
    private NewHealthBar healthBar;

    [SerializeField]
    private NewAdrenalineBar adrenalineBar;

    private Spawn_Manager spawnManager;
    private DynamicDifficultyAdjuster dda;
    private DropSpawner dropSpawner;
    private Player_stats playerStats;

    public AudioSource death;

    public bool isArena = false;
    public bool isBoss = false;

    private bool playerIsDead;

    public bool pcgEnabled = false;
    private GenerateLayout pcgScript;

    void Awake()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<Spawn_Manager>();
        dda = GameObject.Find("DDA").GetComponent<DynamicDifficultyAdjuster>();
        if (dda == null)
            dda = transform.Find("DDA").gameObject.GetComponent<DynamicDifficultyAdjuster>();
        dropSpawner = GameObject.Find("DropManager").GetComponent<DropSpawner>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        death = GetComponent<AudioSource>();

        if (pcgEnabled)
        {
            pcgScript = GetComponent<GenerateLayout>();
            pcgScript.SpawnObstacles();
        } 
    }

    public void SetHealth(float scale)
    {
        healthBar.SetScale(scale);
        //Debug.Log("Here is Scale: " + scale);
    }

    public void SetAdrenaline(float scale)
    {
        adrenalineBar.SetScale(scale);
    }

    public void CreateEnemy(EnemyStats enemy)
    {
        dda.Subscribe(enemy);
        enemy.ChangeDifficulty(dda.GetDifficulty());
    }

    public void KillEnemy(EnemyStats enemy)
    {
        death.Play();
        dda.IncrementKills();
        dda.Unsubscribe(enemy);
        playerStats.AddAdrenaline(enemy.adrenalineYield);
        spawnManager.EnemyDefeated();
        dropSpawner.DropItem(enemy.transform);
    }

    public void KillBoss(EnemyStats enemy)
    {
        Debug.Log("Kill Boss");
        Transform randomDropSpawn = enemy.transform;
        randomDropSpawn.position = new Vector3
            (randomDropSpawn.transform.position.x +1.0f, randomDropSpawn.transform.position.y + 1.0f, randomDropSpawn.transform.position.z);

        death.Play();
        dda.IncrementKills();
        dda.Unsubscribe(enemy);
        playerStats.AddAdrenaline(enemy.adrenalineYield);
        spawnManager.EnemyDefeated();

        // a burst of goodies after the boss death
        for (int i = 0; i < 10; i++)
        {
            randomDropSpawn.position = new Vector3(randomDropSpawn.transform.position.x + Random.Range(-3.0f, 3.0f),
                randomDropSpawn.transform.position.y + Random.Range(-3.0f, 3.0f), randomDropSpawn.transform.position.z);
            dropSpawner.BossDropItem(randomDropSpawn);
        }
        
    }

    public void EnemiesCleared()
    {
        // Arena level complete
        Debug.Log("All enemies defeated!");
        if (!isArena)
        {
            return;
        }
        GameObject.Find("ArenaCanvas").transform.Find("Victory").gameObject.SetActive(true);
        //yield return new WaitForSeconds(2);
        //GoToUpgrade();
    }

    public void GoToUpgrade()
    {
        Debug.Log("Going to upgrade screen.");
        GameObject.Find("StatManager").GetComponent<Stat_Manager>().EndOfLevel();
        SceneManager.LoadScene(1);
    }

    public bool gameIsOver()
    {
        return playerIsDead;
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER!");
        playerIsDead = true;
        Invoke("Menu", endDelay);
    }

    void Menu()
    {
        SceneManager.LoadScene(0);
    }


}
