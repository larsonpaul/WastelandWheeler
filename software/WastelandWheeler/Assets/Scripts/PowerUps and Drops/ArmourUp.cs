using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Change to damage reduction for a limited time
public class ArmourUp : MonoBehaviour
{
    public float duration = 10f;
    private static Player_stats stats;

    private bool used = false;
    private static int active = 0;

    private GameObject icon;

    private GameObject armour;

    // Start is called before the first frame update, get the player's stats
    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Player_stats>();
        icon = GameObject.Find("GameUI").transform.Find("ArmourIcon").gameObject;
        armour = GameObject.Find("Player").transform.Find("ArmourUp").gameObject;
    }

    // Upon collision, check for player tag and set player as thorny
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && used == false)
        {
            used = true;

            StartCoroutine(Armour());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private IEnumerator Armour()
    {
        icon.SetActive(true);

        active += 1;

        armour.SetActive(true);

        stats.isArmoured = true;

        yield return new WaitForSeconds(duration);

        stats.isArmoured = false;

        active -= 1;
        if (active == 0)
        {
            armour.SetActive(false);
            icon.SetActive(false);
        }
        Destroy(gameObject);
    }
}


