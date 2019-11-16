using System.Collections;
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

    void Start()
    {
        spawnManager = transform.Find("Spawn Manager").gameObject.GetComponent<Spawn_Manager>();
        dda = transform.Find("DDA").gameObject.GetComponent<DynamicDifficultyAdjuster>();
        dropSpawner = transform.Find("DropManager").gameObject.GetComponent<DropSpawner>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
    }

    public void SetHealth(float scale)
    {
        healthBar.SetScale(scale);
        Debug.Log("Here is Scale: " + scale);
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
        dda.Unsubscribe(enemy);
        playerStats.AddAdrenaline(enemy.adrenalineYield);
        spawnManager.EnemyDefeated();
        dropSpawner.DropItem(enemy.transform);

    }

    public void EndGame()
    {
        Debug.Log("GAME OVER!");
        Invoke("Menu", endDelay);
    }

    void Menu()
    {
        SceneManager.LoadScene(0);
    }


}
