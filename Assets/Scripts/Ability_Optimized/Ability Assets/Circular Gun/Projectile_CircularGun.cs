using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_CircularGun : MonoBehaviour
{

    //Stats
    [HideInInspector] public int max_bounces = 15;
    [HideInInspector] public float bullet_damage = 1;


    //Counters
    private int curr_bounces;
    private Vector3 last_velocity;

    //References
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        curr_bounces = 0;
    }

    private void LateUpdate()
    {
        last_velocity = rb.velocity;
    }

    public void Shoot(Vector3 dir, float speed)
    {
        rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (curr_bounces >= max_bounces)
            Destroy(gameObject); //make this poolable

        if (collision.gameObject.tag == "Enemy")
        {
            var health = collision.gameObject.GetComponent<HealthObject>();
            var controller = collision.gameObject.GetComponent<UnitController>();

            if (health != null)
                health.TakeDamage(bullet_damage, controller);

        }

        float curr_speed = last_velocity.magnitude;

        Vector3 dir = Vector3.Reflect(last_velocity.normalized, collision.contacts[0].normal);
        rb.velocity = dir * Mathf.Max(curr_speed, 0);

        if(collision.rigidbody != null)
            collision.rigidbody.velocity = Vector3.zero;

        curr_bounces++;
    }


}
