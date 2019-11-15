using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Application.Quit();
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        Debug.Log(Time.fixedDeltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        GameIsPaused = true;
    }
}
