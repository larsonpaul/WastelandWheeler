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
        //Vector3 scale = new Vector3(Mathf.Min(Mathf.Max(x, 0), 1), 1, 1);
        float scale = Mathf.Min(Mathf.Max(x, 0), 1);

        //adrenaline.localScale = scale;
        adrenaline.fillAmount = scale;
    }
}
