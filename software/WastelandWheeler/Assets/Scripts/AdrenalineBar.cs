using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineBar : MonoBehaviour
{
    private Transform bar;

    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetScale(float scale)
    {
        bar.localScale = new Vector3(Mathf.Min(Mathf.Max(scale, 0), 1), 1f);
    }
}
