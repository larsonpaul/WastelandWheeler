using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateValues : MonoBehaviour
{
    private Stat_Manager stats;
    private float currCoins;
    private float betterROF;

    [SerializeField]
    TextMeshProUGUI oldHealth;
    //[SerializeField]
    //TextMeshProUGUI oldSpeed;
    [SerializeField]
    TextMeshProUGUI oldROF;
    [SerializeField]
    TextMeshProUGUI oldDamage;
    [SerializeField]
    TextMeshProUGUI oldBullets;
    [SerializeField]
    TextMeshProUGUI newHealth;
    //[SerializeField]
    //TextMeshProUGUI newSpeed;
    [SerializeField]
    TextMeshProUGUI newROF;
    [SerializeField]
    TextMeshProUGUI newDamage;
    [SerializeField]
    TextMeshProUGUI newBullets;
    [SerializeField]
    TextMeshProUGUI tokensLeft;

    [SerializeField]
    TextMeshProUGUI healthCost;
    [SerializeField]
    TextMeshProUGUI rofCost;
    [SerializeField]
    TextMeshProUGUI damageCost;
    [SerializeField]
    TextMeshProUGUI bulletCost;

    private void Start()
    {
        stats = Stat_Manager.Instance;
        betterROF = stats.GetROF() * 100;
        
        oldHealth.text = stats.GetMaxHealth().ToString();

        //oldSpeed.text = stats.GetSpeed().ToString();

        oldROF.text = betterROF.ToString("F2");

        oldDamage.text = stats.GetDamage().ToString();

        oldBullets.text = stats.GetBulletSize().ToString("F2");
    }
    void Update()
    {
        currCoins = stats.GetCoins();
        betterROF = stats.GetROF() * 100;

        tokensLeft.text = "Tokens: " + currCoins.ToString();

        newHealth.text = stats.GetMaxHealth().ToString();

        //newSpeed.text = stats.GetSpeed().ToString();

        newROF.text = betterROF.ToString("F2");

        newDamage.text = stats.GetDamage().ToString();

        newBullets.text = stats.GetBulletSize().ToString("F2");

        if(stats.GetMaxHealth() >= 200)
        {
            healthCost.text = "Cost: 2";
        }
        // Most recent change
        if (stats.GetROF() <= .15f)
        {
            rofCost.text = "Cost: 2";
        }
        if (stats.GetDamage() >= 15f)
        {
            damageCost.text = "Cost: 2";
        }
        if (stats.GetBulletSize() >= 1.5f)
        {
            bulletCost.text = "Cost: 2";
        }
    }
}
