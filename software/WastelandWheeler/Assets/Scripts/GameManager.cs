using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float endDelay = 3f;


    [SerializeField] private HealthBar healthBar;

    public void SetHealth(float scale)
    {
        healthBar.SetScale(scale);
    }

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
