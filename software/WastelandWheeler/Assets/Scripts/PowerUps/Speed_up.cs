using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that controls the player's move speed
 */
public class Speed_up : MonoBehaviour
{
    public float multiplier = 1.5f;
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;
    private static int active = 0;

    private GameObject icon;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        icon = GameObject.Find("GameUI").transform.Find("SpeedIcon").gameObject;
    }

    // On collision, check the player tag and increase the move speed based on a multiplier
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
        icon.SetActive(true);
        active += 1;

        stats.move_speed *= multiplier;

        yield return new WaitForSeconds(duration);

        stats.move_speed /= multiplier;

        active -= 1;
        if (active == 0) icon.SetActive(false);
        Destroy(gameObject);
    }
}