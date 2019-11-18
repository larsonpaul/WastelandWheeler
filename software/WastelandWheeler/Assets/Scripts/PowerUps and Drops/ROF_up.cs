using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that controls the player's ROF
 */
public class ROF_up : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;
    private static int active = 0;

    private GameObject icon;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        icon = GameObject.Find("GameUI").transform.Find("ROFIcon").gameObject;
    }

    // On collision, check the player tag and increase the ROF based on a multiplier
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;

            StartCoroutine(Power());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private IEnumerator Power()
    {
        active += 1;

        if (active == 1)
        {
            stats.rate_of_fire /= multiplier;
            icon.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        active -= 1;
        if (active == 0)
        {
            stats.rate_of_fire *= multiplier;
            icon.SetActive(false);
        }
        Destroy(gameObject);
    }
}