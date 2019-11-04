using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToUpgrade : MonoBehaviour
{
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        // when there are no enemies do this
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_stats>().move_speed = 0f;
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(2);
        }

        // if not return 
    }
}