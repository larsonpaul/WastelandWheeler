using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Play_game()
    {
        if (gameObject.CompareTag("Top_down"))
        {
            SceneManager.LoadScene(1);
        }
        if (gameObject.CompareTag("Platformer"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
