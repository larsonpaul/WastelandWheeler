using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToUpgrade : MonoBehaviour
{
    [SerializeField]
    private bool waves_defeated = true;
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        // when there are no enemies do this
        if (collision.CompareTag("Player") && waves_defeated)
        {
            yield return new WaitForSeconds(2);
            GameObject.Find("StatManager").GetComponent<Stat_Manager>().EndOfLevel(); 
            SceneManager.LoadScene(1);
        }

        // if not return 
    }

    public void LevelComplete()
    {
        waves_defeated = true;
    }
}