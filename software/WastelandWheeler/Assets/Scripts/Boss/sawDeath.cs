
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawDeath : MonoBehaviour
{

	Player_stats playerStats;   // used to deal damage

	// Start is called before the first frame update
	void Start()
	{
        playerStats = FindObjectOfType<Player_stats>();

	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		GameObject obj = other.gameObject;

		// cases where bullet is not destroyed
		string[] tags = { "Enemy", "Power_Up", "bullet" };
		for (int i = 0; i < tags.Length; i++)
		{
			if (obj.CompareTag(tags[i])) return;
		}
		// cases where bullet is destroyed
		if (obj.CompareTag("Player"))
		{
            playerStats.RemoveHealth(playerStats.healthCurrent);
            return;
		}

		else
		{
			return;
		}

	}

}
