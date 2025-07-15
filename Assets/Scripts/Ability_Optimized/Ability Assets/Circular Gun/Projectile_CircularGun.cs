using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_CircularGun : MonoBehaviour
{

    //Stats
    [HideInInspector] public int max_bounces = 15;
    [HideInInspector] public float bullet_damage = 1;

    private float fixed_speed;


    //Counters
    private int curr_bounces;
    private Vector3 last_velocity;
    private List<Collider> ignore_list = new();

    //References
    private Rigidbody rb;
    private Collider coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        curr_bounces = 0;
    }

    private void LateUpdate()
    {
        last_velocity = rb.velocity.normalized * fixed_speed;
    }

    public void Shoot(Vector3 dir, float speed)
    {
        rb.velocity = dir * speed;
        fixed_speed = speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (curr_bounces >= max_bounces)
            Destroy(gameObject); //make this poolable


        switch (collision.gameObject.tag)
        {
            case "Enemy":


                var health = collision.gameObject.GetComponent<HealthObject>();
                var controller = collision.gameObject.GetComponent<UnitController>();

                if (health != null)
                    health.TakeDamage(bullet_damage, controller);

                if (collision.gameObject.GetComponent<BossController>() != null)
                    goto default;
                else goto case "Player";

            case "Player":

                Physics.IgnoreCollision(collision.collider, coll);
                if (collision.rigidbody != null)
                    collision.rigidbody.velocity = Vector3.zero;
                rb.velocity = last_velocity;
                ignore_list.Add(collision.collider);

                break;

            default:

                float curr_speed = last_velocity.magnitude;

                Vector3 dir = Vector3.Reflect(last_velocity.normalized, collision.contacts[0].normal);
                rb.velocity = dir * Mathf.Max(curr_speed, 0);

                if (collision.rigidbody != null)
                    collision.rigidbody.velocity = Vector3.zero;

                curr_bounces++;

                foreach (var c in ignore_list)
                    Physics.IgnoreCollision(c, coll, false);

                ignore_list.Clear();
             break;
        }

     
    }


}
