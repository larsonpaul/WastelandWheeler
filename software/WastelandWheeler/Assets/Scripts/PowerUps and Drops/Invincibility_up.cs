using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that will make the player invincible for a limited duration
 */
public class Invincibility_up : MonoBehaviour
{
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;

    [SerializeField]
    private static int active = 0;

    private GameObject icon;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        icon = GameObject.Find("GameUI").transform.Find("InvincibleIcon").gameObject;
    }

    // Upon collision, check for player tag and set player invinciblity
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            stats.playPowerup = true;
            StartCoroutine(Power());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private IEnumerator Power()
    {
        if (!stats.isInvincible) active = 0;

        active += 1;

        stats.isInvincible = true;
        icon.SetActive(true);

        yield return new WaitForSeconds(duration);

        active -= 1;
        if (active == 0)
        {
            stats.isInvincible = false;
            icon.SetActive(false);
        }

        Destroy(gameObject);
    }
}
