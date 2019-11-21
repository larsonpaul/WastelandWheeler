using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHealthBar : MonoBehaviour
{
    private Image health;

    // Start is called before the first frame update
    void Awake()
    {
        health = transform.Find("Health").gameObject.GetComponent<Image>();
    }

    public void SetScale(float x)
    {
        float scale = Mathf.Clamp(x, 0, 1);

        health.fillAmount = scale;
    }
}
