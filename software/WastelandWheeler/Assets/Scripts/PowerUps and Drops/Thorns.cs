using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that will make the player thorny for a limited duration (damage in EnemyDamage.cs)
 */
public class Thorns : MonoBehaviour
{
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;
    private static int active = 0;

    private GameObject icon;

    private GameObject thorns;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        icon = GameObject.Find("GameUI").transform.Find("ThornsIcon").gameObject;
        thorns = GameObject.Find("Player").transform.Find("Thorns").gameObject;
    }

    // Upon collision, check for player tag and set player as thorny
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

        thorns.SetActive(true);

        stats.isThorny = true;

        yield return new WaitForSeconds(duration);

        stats.isThorny = false;

        active -= 1;
        if (active == 0)
        {
            thorns.SetActive(false);
            icon.SetActive(false);
        }
        Destroy(gameObject);
    }
}
