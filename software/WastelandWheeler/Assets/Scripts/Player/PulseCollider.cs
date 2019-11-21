using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseCollider : MonoBehaviour
{
    private Vector3 ppos;

    [SerializeField]
    private float power = 20;

    private const float MINSCALE = 2.0f;
    private const float MULTIPLIER = 4.0f;

    private float pulseDuration = .25f;

    // Start is called before the first frame update
    //void Awake()
    //{
    //    ppos = transform.parent.position;
    //}

    void FixedUpdate()
    {
        float newScale = transform.localScale.x + (MaxScale() / pulseDuration) * Time.fixedDeltaTime;

        if (newScale > MaxScale())
        {
            gameObject.SetActive(false);
            return;
        }

        transform.localScale = new Vector3(newScale, newScale);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(MINSCALE, MINSCALE);
    }

    private float MaxScale()
    {
        return MINSCALE * MULTIPLIER;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            Debug.Log("Collision with " + col.gameObject.name);
            Rigidbody2D enemyRB = obj.GetComponent<Rigidbody2D>();
            Vector3 ppos = transform.parent.position;

            Debug.Log("Enemy  " + obj.transform.position);
            Debug.Log("Player " + ppos);
            Vector2 force = (obj.transform.position - ppos).normalized * power;

            enemyRB.AddForce(force);
        }
        else if (obj.CompareTag("bullet"))
        {
            Destroy(obj);
        }
    }
}
