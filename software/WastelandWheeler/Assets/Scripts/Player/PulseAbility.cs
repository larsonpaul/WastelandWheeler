using UnityEngine;

public class PulseAbility : MonoBehaviour
{
    private Player_stats stats;

    private PulseCollider collider;

    private float cost = 50.0f;

    private bool onCooldown = false;
    private float cooldownLength = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<Player_stats>();
        collider = transform.Find("Pulse").gameObject.GetComponent<PulseCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stats.GetAdrenaline() >= cost && !onCooldown)
        {
            onCooldown = true;

            // functionality
            collider.Activate();

            // cost and cooldown
            stats.RemoveAdrenaline(cost);
            Invoke("Cooldown", cooldownLength);
        }
    }

    private void Cooldown()
    {
        onCooldown = false;
    }
}
