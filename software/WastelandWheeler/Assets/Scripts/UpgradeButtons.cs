﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    private Player_stats stats = Player_stats.Instance;
    public float health = 10f;
    public float speed = 2f;
    public float ROF = .2f;
    public float damage = 2f;
    public float bulletSize = 1.1f;
    private float currCoins;

    void Update()
    {
        currCoins = stats.GetCoins();
    }
    public void AddHealthVal()
    {
        if (currCoins >= 1)
        {
            stats.SetHealth(health);
            MinusOneToken();
        }
    }

    public void AddSpeedVal()
    {
        if (currCoins >= 1)
        {
            stats.SetSpeed(speed);
            MinusOneToken();
        }
    }

    public void AddROFVal()
    {
        if (currCoins >= 1)
        {
            stats.SetROF(ROF);
            MinusOneToken();
        }
    }

    public void AddDamageVal()
    {
        if (currCoins >= 1)
        {
            stats.SetDamage(damage);
            MinusOneToken();
        }
    }

    public void AddBulletSizeVal()
    {
        if (currCoins >= 1)
        {
            stats.SetBulletSize(bulletSize);
            MinusOneToken();
        }
    }

    public void MinusOneToken()
    {
        stats.SetCoins(1);
    }
}
