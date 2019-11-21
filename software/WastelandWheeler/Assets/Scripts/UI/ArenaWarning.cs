using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWarning : MonoBehaviour
{
    private float duration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideWarning());
    }

    private IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
