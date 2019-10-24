using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private Transform bar;
    private Transform healBar;
    private Transform damageBar;

    void Start()
    {
        bar = transform.Find("Bar");
        healBar = transform.Find("HealBar");
        damageBar = transform.Find("DamageBar");
    }

    public void SetScale(float x)
    {
        Vector3 scale = new Vector3(Mathf.Min(Mathf.Max(x, 0), 1), 1f);

        if (bar.localScale.x == x)
            return;
        else if (x > bar.localScale.x)
            StartCoroutine(Heal(scale));
        else if (x < bar.localScale.x)
            StartCoroutine(Damage(scale));

        //bar.localScale = new Vector3(Mathf.Min(Mathf.Max(scale, 0), 1), 1f);
    }

    IEnumerator Heal(Vector3 scale)
    {
        healBar.localScale = scale;
        damageBar.localScale = scale;
        yield return new WaitForSeconds(0.25f);
        bar.localScale = scale;
    }

    IEnumerator Damage(Vector3 scale)
    {
        bar.localScale = scale;
        healBar.localScale = scale;
        yield return new WaitForSeconds(0.25f);
        damageBar.localScale = scale;
    }


}
