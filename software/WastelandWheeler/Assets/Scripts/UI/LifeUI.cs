using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private int lives;

    private Player_stats player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player_stats>();
    }

    public void UpdateUI()
    {
        lives = player.GetLives();
    
        text.text = lives.ToString();
        
    }
}
