using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : MonoBehaviour
{
    public float duration = 7f;
    private static Player_stats stats;
    private PlayerCreep creep;

    private bool used = false;
    private static int active = 0;

    private GameObject icon;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        creep = GameObject.FindWithTag("Player").GetComponent<PlayerCreep>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;
            StartCoroutine(FlameOn());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private IEnumerator FlameOn()
    {
        if (!stats.onFire) active = 0;

        active += 1;

        if (active == 1)
        {
            stats.onFire = true;
        }

        yield return new WaitForSeconds(duration);

        active -= 1;
        if (active == 0)
        {
            stats.onFire = false;
        }
        Destroy(gameObject);
    }
}
