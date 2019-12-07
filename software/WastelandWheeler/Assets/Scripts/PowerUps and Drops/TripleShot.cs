using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    public float duration = 10f;

    private bool used = false;
    private static int active = 0;

    private GameObject icon;

    private static Player_stats stats;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        icon = GameObject.Find("GameUI").transform.Find("TripleShotIcon").gameObject;
    }

    // Upon collision, check for player tag and set player as tripleShot
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            stats.playPowerup = true;
            StartCoroutine(Tripler());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private IEnumerator Tripler()
    {
        if (!stats.tripleShot) active = 0;

        active++;

        stats.tripleShot = true;
        icon.SetActive(true);

        yield return new WaitForSeconds(duration);

        if (--active == 0)
        {
            stats.tripleShot = false;
            icon.SetActive(false);
        }
        Destroy(gameObject);
    }
}
