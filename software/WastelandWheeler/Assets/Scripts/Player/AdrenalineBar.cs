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

    public void SetScale(float x)
    {
        Vector3 scale = new Vector3(Mathf.Min(Mathf.Max(x, 0), 1), 1f);

        if (bar.localScale.x == x)
            return;
        else if (x > bar.localScale.x)
            StartCoroutine(AddAdrenaline(scale));
        else if (x < bar.localScale.x)
            StartCoroutine(RemoveAdrenaline(scale));
    }

    IEnumerator AddAdrenaline(Vector3 scale)
    {
        yield return new WaitForSeconds(0.1f);
        bar.localScale = scale;
    }

    IEnumerator RemoveAdrenaline(Vector3 scale)
    {
        yield return new WaitForSeconds(0.1f);
        bar.localScale = scale;
    }
}
