using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdaDisplay : MonoBehaviour
{
    // used to turn display on and off
    public bool DdaDisplayEnabled = false;

    private DynamicDifficultyAdjuster dda;
    private int DdaDifficulty = 0;

    // values needed to display how DDA affects enemies
    private float enemyHealthScaler = 0f;  // affects how much health an enemy receives
    private float enemySpeedScaler = 0f; // affects how fast enemies move
    private float enemyFirerateScaler = 0f; // affects enemy rate of fire
    private float enemyDamageScaler = 0f; // affects how much damage enemies do

    // values needed to display how DDA affects the player
    private float playerDamageScaler = 0f; // affects how much damage the player takes
    private float playerAdrenalineScaler = 0f; // affects how much adrenaline the player receives when killing an enemy
    private float playerHealScaler = 0f; // affects how much the player heals



    // Start is called before the first frame update
    void Start()
    {
        dda = DynamicDifficultyAdjuster.Instance;
    }

    // Update is called once per frame
    void UpdateDisplay()
    {
        // find out what the difficulty is 
        DdaDifficulty = dda.GetDifficulty();

        // update the displayed values on the canvas
        // enemy values 
        enemyHealthScaler = 1.0f + (0.05f * DdaDifficulty);
        enemySpeedScaler = 1.0f + (0.05f * DdaDifficulty);
        enemyFirerateScaler = 1.0f + (0.05f * DdaDifficulty);
        enemyDamageScaler = 1.0f + (0.05f * DdaDifficulty);

        // player values 
        playerDamageScaler = 1.0f + (0.1f * DdaDifficulty);
        playerAdrenalineScaler = 1.0f + (-0.05f * DdaDifficulty);
        playerHealScaler = 1.0f - (0.01f * DdaDifficulty);
    }
}
