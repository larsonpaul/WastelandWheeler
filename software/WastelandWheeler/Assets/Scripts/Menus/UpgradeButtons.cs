using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    private Stat_Manager stats;
    public float health = 10f;
    //public float speed = 2f;
    public float ROF = .98f;
    public float damage = 1f;
    public float bulletSize = 1.02f;
    private float currCoins;

    void Start()
    {
        stats = GameObject.Find("StatManager").GetComponent<Stat_Manager>();
    }

    void Update()
    {
        currCoins = stats.GetCoins();
    }

    //public void AddSpeedVal()
    //{
    //    if (currCoins >= 1)
    //    {
    //        stats.SetSpeed(speed);
    //        MinusOneToken();
    //    }
    //}

    public void AddHealthVal()
    {
        if (currCoins >= 2 && stats.GetMaxHealth() >= 200f)
        {
            stats.SetMaxHealth(health);
            MinusOneToken(2);
        }
        else if (currCoins >= 1)
        {
            if (stats.GetMaxHealth() >= 200f) return;
            else
            {
                stats.SetMaxHealth(health);
                MinusOneToken(1);
            }
        }
    }

    public void AddROFVal()
    {
        if (currCoins >= 2 && stats.GetROF() <= .15f)
        {
            stats.SetROF(ROF);
            MinusOneToken(2);
        }
        else if (currCoins >= 1)
        {
            if (stats.GetROF() <= .1f) return;
            else
            {
                stats.SetROF(ROF);
                MinusOneToken(1);
            }
        }
    }

    public void AddDamageVal()
    {
        if (currCoins >= 2 && stats.GetDamage() >= 15f)
        {
            stats.SetDamage(damage);
            MinusOneToken(2);
        }
        else if (currCoins >= 1 )
        {
            if (stats.GetDamage() >= 15f) return;
            else
            {
                stats.SetDamage(damage);
                MinusOneToken(1);
            }
        }
    }

    public void AddBulletSizeVal()
    {
        if (currCoins >= 2 && stats.GetBulletSize() >= 1.5f)
        {
            stats.SetBulletSize(bulletSize);
            MinusOneToken(2);
        }
        else if (currCoins >= 1)
        {
            if (stats.GetBulletSize() >= 1.5f) return;
            else
            {
                stats.SetBulletSize(bulletSize);
                MinusOneToken(1);
            }
        }
    }

    public void MinusOneToken(float tokens)
    {
        stats.SetCoins(tokens);
    }
}
