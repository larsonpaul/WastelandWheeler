using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance
    //{
    //    get;
    //    set;
    //}

    //void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}

    public float endDelay = 3f;

    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AdrenalineBar adrenalineBar;

    public void SetHealth(float scale)
    {
        healthBar.SetScale(scale);
        Debug.Log("Here is Scale: " + scale);
    }

    public void SetAdrenaline(float scale)
    {
        adrenalineBar.SetScale(scale);
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
