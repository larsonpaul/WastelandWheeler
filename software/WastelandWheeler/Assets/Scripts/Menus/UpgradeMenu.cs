using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    public void StartNextLevel()
    {
        // If scene was topdown, load platformer
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;

        // Else load topdown
        /* Don't currently know how to check previous scene */
    }

}
