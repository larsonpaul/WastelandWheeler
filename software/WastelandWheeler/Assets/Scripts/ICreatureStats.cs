using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatureStats
{

    float GetHealth();

    void AddHealth(float num);

    void RemoveHealth(float num);

    float GetSpeed();

    void ModifySpeed(float mod);

    float GetFirerate();

    void ModifyFirerate(float mod);

    void OnDeath();
}
