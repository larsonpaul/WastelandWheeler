using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDifficultyAdjuster : MonoBehaviour
{
    private static DynamicDifficultyAdjuster instance;
    private HashSet<MonoBehaviour> subscribers;
    private float value;
    private float startTime = 0.0f;
    private float currentTime;
    private float CHECK_INTERVAL = 5.0f; // number of seconds between running scripts

    private GameObject player;
    private Player_stats player_stats;
    private GameObject boss;

    // used in adjusting difficulty
    private float lastPlayerHealth;
    private float lastBossHealth;
    //private float lastSceneProgress;
    public static DynamicDifficultyAdjuster Instance
    {
        get
        {
            return instance;
            
        }
    }

    private void Awake()
    {
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
        subscribers = new HashSet<MonoBehaviour>();
        startTime = Time.time;
        player = GameObject.Find("Player");
        player_stats = player.GetComponent<Player_stats>();
        lastPlayerHealth = player_stats.healthMax;
        //boss = GameObject.Find("Boss");
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


    

    private float curPlayerHealth;
    private float curBossHealth;
    //private float curSceneProgress;

    private float playerMaxHealth;
    private float bossMaxHealth;
    private float percentTotalHealth = 0.5f;
    private float percentTotalBossHealth = 0.5f;
    // Method that sends message to update difficulty
    private void UpdateDifficulty()
    {
        // run algorithm to determine if game needs to be harder or easier 
        // assign a numeric amount to value 
        curPlayerHealth = player_stats.GetHealth();
        //curBossHealth = boss.GetHealth();
        playerMaxHealth = player_stats.healthMax;
        //sbossMaxHealth = boss.healthMax;
        if (lastPlayerHealth - curPlayerHealth > percentTotalHealth*playerMaxHealth)
        {
            // player losing health too quickly, make the game easier!
            value = -10.0f; // ten percent easier
        }
        //else if (lastBossHealth - curBossHealth > percentTotalBossHealth * bossMaxHealth)
        //{
            // boss dying to quick, make game harder 
            //value = 10.0f; // ten percent harder
        //}
        else
        {
            value = 0.0f;
        }

        // publish that change to subscribers
        if (value != 0)
        {
            foreach (MonoBehaviour g in subscribers)
            {
                g.SendMessage("ChangeDifficulty", value);
            }
        }

        lastPlayerHealth = curPlayerHealth;
        //lastBossHealth = curBossHealth;
        //lastSceneProgress = curSceneProgress;

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
}

