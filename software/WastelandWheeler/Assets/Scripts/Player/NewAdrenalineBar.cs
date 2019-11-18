using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewAdrenalineBar : MonoBehaviour
{
    private Image adrenaline;

    // Start is called before the first frame update
    void Start()
    {
        adrenaline = transform.Find("Adrenaline").gameObject.GetComponent<Image>();
    }

    public void SetScale(float x)
    {
        float scale = Mathf.Clamp(x, 0, 1);

        adrenaline.fillAmount = scale;
    }
}
