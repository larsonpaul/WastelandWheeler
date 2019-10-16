using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * Class that runs the MainMenu scene by loading the other game scenes
 */
public class MainMenu : MonoBehaviour
{
    // Loads scene one in the build
    public void StartTopdown()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    // Loads scene two in the build
    public void StartPlatformer()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    // Stop application 
    public void QuitGame()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }

}
