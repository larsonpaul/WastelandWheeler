using UnityEngine;

public class PulseAbility : MonoBehaviour
{
    private Player_stats stats;

    private PulseCollider collider;

    private float cost = 50.0f;

    private float curCooldown = 0.0f;
    private float maxCooldown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<Player_stats>();
        collider = transform.Find("Pulse").gameObject.GetComponent<PulseCollider>();
    }

    void FixedUpdate()
    {
        if (curCooldown > 0)
        {
            curCooldown = Mathf.Max(0, curCooldown - Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curCooldown <= 0 && Input.GetKeyDown(KeyCode.Space) && stats.GetAdrenaline() >= cost)
        {
            curCooldown = maxCooldown;

            // functionality
            collider.Activate();

            // cost and cooldown
            stats.RemoveAdrenaline(cost);
        }
    }

    public void Respawn()
    {
        curCooldown = maxCooldown;
        collider.Activate();
    }
}
