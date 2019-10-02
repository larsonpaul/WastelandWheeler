using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartTopdown()
    {
        SceneManager.LoadScene(1);
    }

    public void StartPlatformer()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }

}
