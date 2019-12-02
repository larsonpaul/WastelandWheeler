using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToUpgrade : MonoBehaviour
{
    [SerializeField]
    private bool waves_defeated = true;

    private LevelChanger fade_animator;

    void Start()
    {
        fade_animator = FindObjectOfType<LevelChanger>();   
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        // when there are no enemies do this
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("StatManager").GetComponent<Stat_Manager>().EndOfLevel();
            fade_animator.FadeToPlatformer();
            yield return new WaitForSeconds(1);
        }

        // if not return 
    }

    public void LevelComplete()
    {
        waves_defeated = true;
    }
}