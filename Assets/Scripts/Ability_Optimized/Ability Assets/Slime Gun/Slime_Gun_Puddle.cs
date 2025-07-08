using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Gun_Puddle : DetectionSphere
{

    [HideInInspector] public float damage_over_time;
    [HideInInspector] public float dot_interval;
    [HideInInspector] public float puddle_lifetime;
    [HideInInspector] public float slow_power;
    [HideInInspector] public UnitController source;
    [HideInInspector] public Vector3 target_scale;
    [HideInInspector] public float scale_speed;


    private float time_alive=0;
    private float dot_timer=0;

    private Vector3 original_scale;
    private float destroy_mag;
    private bool to_destroy = false;

    private void Start()
    {
        original_scale = transform.localScale;
        original_scale.y = 1;
        destroy_mag = Vector3.SqrMagnitude(original_scale);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (whitelist.Contains(other.tag))
        {
            inside.Add(other.gameObject);
            var rb = other.GetComponent<Rigidbody>();
            rb.drag = slow_power;
        }
           
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (inside.Contains(other.gameObject))
        {
            inside.Remove(other.gameObject);
            var rb = other.GetComponent<Rigidbody>();
            rb.drag = 0;
        }
            
    }

    private void Update()
    {
        if (to_destroy)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, original_scale, Time.deltaTime * scale_speed);
            if(Vector3.SqrMagnitude(transform.localScale) <= destroy_mag)
                Destroy(gameObject);
            return;
        }

        time_alive += Time.deltaTime;

        transform.localScale = Vector3.MoveTowards(transform.localScale, target_scale, Time.deltaTime * scale_speed * 4);

        if(time_alive >= puddle_lifetime)
        {
            to_destroy = true;
            foreach (var e in inside)
            {
                var rb = e.GetComponent<Rigidbody>();
                rb.drag = 0;
            }
            return;
        }

        dot_timer += Time.deltaTime;

        if(dot_timer >= dot_interval)
        {
            foreach(var e in inside)
            {
                var health = e.GetComponent<HealthObject>();
                health.TakeDamage(damage_over_time, source);
            }
            dot_timer = 0;
        }

    }


 

}
