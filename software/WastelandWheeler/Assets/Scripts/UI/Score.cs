using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    private float score;

    void Start()
    {
        score = GameObject.Find("StatManager").GetComponent<Stat_Manager>().GetScore();   
    }

    public void UpdateScore(float scoreChange)
    {
        score += scoreChange;
        textBox.text = "Score: " + score;
    }

    public float GetScore()
    {
        return score;
    }
}
