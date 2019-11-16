using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDifficultyAdjuster : MonoBehaviour
{
    private static DynamicDifficultyAdjuster instance;
    private HashSet<MonoBehaviour> subscribers;
    
    private float startTime = 0.0f;
    private float currentTime;
    private const float CHECK_INTERVAL = 1.5f; // number of seconds between poling the game for how well player is doing

    private GameObject player;
    private Player_stats player_stats;
    private BossFightOne2D boss_stats;
    private GameObject boss;

    // used in adjusting difficulty
    private float lastPlayerHealth;
    private float lastBossHealth;
    private float maxBossHealth;
    //private float lastSceneProgress;

    private int enemiesKilled = 0;

    [SerializeField]
    private int difficulty_level;

    public static DynamicDifficultyAdjuster Instance
    {
        get
        {
            return instance;
            
        }
    }

    private void Awake()
    {
        subscribers = new HashSet<MonoBehaviour>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        difficulty_level = 0;
        startTime = Time.time;

        player = GameObject.Find("Player");
        player_stats = player.GetComponent<Player_stats>();
        lastPlayerHealth = player_stats.healthMax;

        boss = GameObject.Find("Boss");
        if (boss != null)
        {
            boss_stats= boss.GetComponent<BossFightOne2D>();
            maxBossHealth = boss_stats.bossHealth;
            lastBossHealth = maxBossHealth;
        }
        //lastBossHealth = boss.healthMax;
        //lastSceneProgress = 0.0f;

    }

    // on subscriber's Awake() or Start() they should subscribe to this list
    public void Subscribe(MonoBehaviour sub) 
    {
        subscribers.Add(sub);
    }
    // private DynamicDifficultyAdjuster dda = DynamicDifficultyAdjuster.Instance();
    // dda.Subscribe();


    // before subscriber is destroyed they should unsubscribe from this list
    public void Unsubscribe(MonoBehaviour sub)
    {
        subscribers.Remove(sub);
    }

    public int GetDifficulty()
    {
        return difficulty_level;
    }

    

    private float curPlayerHealth;
    private float curBossHealth;
    //private float curSceneProgress;

    private float playerMaxHealth;
    private float percentTotalHealth = 0.15f;
    private float percentTotalBossHealth = 0.10f;
    
    private void UpdateDifficulty()
    {
        // run algorithm to determine if game needs to be harder or easier 
        curPlayerHealth = player_stats.GetHealth();
        playerMaxHealth = player_stats.healthMax;

        if (boss_stats != null)
        {
            curBossHealth = boss_stats.bossHealth;
            lastBossHealth = curBossHealth;
        }

        // Decreases: Events that will decrease the difficulty
        if (difficulty_level > -5)
        {
            // Is the player taking too much damage?
            if (lastPlayerHealth - curPlayerHealth > percentTotalHealth * playerMaxHealth)
            {
                difficulty_level--;
                UpdateSubscribers();
            }
        }

        // Increases: Events that will increase the difficulty
        if (difficulty_level < 5)
        {
            // Is the boss taking too much damage?
            if (lastBossHealth - curBossHealth > percentTotalBossHealth * maxBossHealth)
            {
                difficulty_level++;
                UpdateSubscribers();
            }

            // Has the player killed many enemies?
            if (enemiesKilled > 10 + difficulty_level)
            {
                enemiesKilled = 0;
                difficulty_level++;
                UpdateSubscribers();
            }
        }

        // set values for next loop
        lastPlayerHealth = curPlayerHealth;
        lastBossHealth = curBossHealth;
        //lastSceneProgress = curSceneProgress;
    }

    // Method that sends message to update difficulty
    private void UpdateSubscribers()
    {
        foreach (MonoBehaviour g in subscribers)
        {
            g.SendMessage("ChangeDifficulty", difficulty_level);
        }
    }

    public void Update()
    {
        currentTime = Time.time;
        if (currentTime - startTime > CHECK_INTERVAL)
        {
            // now check state of how the player is doing
            // and decide if game needs to be adjusted 
            startTime = currentTime;
            UpdateDifficulty();
        }
    }
    // need some way of tracking game difficulty and some way 

    public void IncrementKills(int kills = 1)
    {
        enemiesKilled += kills;
    }
}

