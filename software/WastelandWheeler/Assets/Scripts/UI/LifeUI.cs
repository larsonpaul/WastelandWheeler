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

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player_stats>();
        lives = player.GetLives();
    }

    public void UpdateUI()
    {
        lives = player.GetLives();
    
        text.text = lives.ToString();
        
    }
}
