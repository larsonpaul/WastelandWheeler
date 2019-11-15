using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHealthBar : MonoBehaviour
{
    private Image health;

    // Start is called before the first frame update
    void Start()
    {
        health = transform.Find("Health").gameObject.GetComponent<Image>();
    }

    public void SetScale(float x)
    {
        //Vector3 scale = new Vector3(Mathf.Min(Mathf.Max(x, 0), 1), 1, 1);
        float scale = Mathf.Min(Mathf.Max(x, 0), 1);

        //if (health.localScale.x == x)
        //    return;
        //else if (health.localScale.x < x)
        //    StartCoroutine(Heal(scale));
        //else if (health.localScale.x > x)
        //    StartCoroutine(Damage(scale));


        //health.localScale = scale;
        health.fillAmount = scale;

    }

    //IEnumerator Heal(Vector3 scale)
    //{
    //    healBar.localScale = scale;
    //    damageBar.localScale = scale;
    //    yield return new WaitForSeconds(0.25f);
    //    bar.localScale = scale;
    //}

    //IEnumerator Damage(Vector3 scale)
    //{
    //    bar.localScale = scale;
    //    healBar.localScale = scale;
    //    yield return new WaitForSeconds(0.25f);
    //    damageBar.localScale = scale;
    //}

}
