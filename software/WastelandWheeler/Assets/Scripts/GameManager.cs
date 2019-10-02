using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float endDelay = 3f;

    public void EndGame()
    {
        Debug.Log("GAME OVER!");
        Invoke("Menu", endDelay);
    }

    void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
