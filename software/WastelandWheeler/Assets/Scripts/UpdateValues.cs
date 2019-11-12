using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateValues : MonoBehaviour
{
    private Stat_Manager stats;
    private float currCoins;

    [SerializeField]
    TextMeshProUGUI oldHealth;
    [SerializeField]
    TextMeshProUGUI oldSpeed;
    [SerializeField]
    TextMeshProUGUI oldROF;
    [SerializeField]
    TextMeshProUGUI oldDamage;
    [SerializeField]
    TextMeshProUGUI oldBullets;
    [SerializeField]
    TextMeshProUGUI newHealth;
    [SerializeField]
    TextMeshProUGUI newSpeed;
    [SerializeField]
    TextMeshProUGUI newROF;
    [SerializeField]
    TextMeshProUGUI newDamage;
    [SerializeField]
    TextMeshProUGUI newBullets;
    [SerializeField]
    TextMeshProUGUI tokensLeft;

    private void Start()
    {
        stats = Stat_Manager.Instance;
        oldHealth.text = stats.GetMaxHealth().ToString();

        oldSpeed.text = stats.GetSpeed().ToString();

        oldROF.text = stats.GetROF().ToString();

        oldDamage.text = stats.GetDamage().ToString();

        oldBullets.text = stats.GetBulletSize().ToString();
    }
    void Update()
    {
        currCoins = stats.GetCoins();

        tokensLeft.text = "Tokens: " + currCoins.ToString();

        newHealth.text = stats.GetMaxHealth().ToString();

        newSpeed.text = stats.GetSpeed().ToString();

        newROF.text = stats.GetROF().ToString();

        newDamage.text = stats.GetDamage().ToString();

        newBullets.text = stats.GetBulletSize().ToString();
    }
}
