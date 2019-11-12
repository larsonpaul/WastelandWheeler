﻿using System.Collections;
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
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(1);
        }

        // if not return 
    }
}