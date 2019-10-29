using UnityEngine;
using System.Collections;

public interface IDiffcultyAdjuster
{
    // method to affect the game making it easier or hard
    // negative amounts make the game easier, positive make it harder
    void ChangeDifficulty(float amount);
}
