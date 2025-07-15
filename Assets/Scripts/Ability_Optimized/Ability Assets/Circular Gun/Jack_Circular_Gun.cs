using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Circular_Gun : MonoBehaviour
{

    public float detonation_delay = 3f;
    public float detonation_trigger_distance = 5f;
    public float knockback_force = 10f;
    public float explosion_damage = 5;
    public UnitController owner;

    private bool ticking_down = false;
    [SerializeField] private List<EnemyController> enemies = new();
    private float detonation_timer = 0;

    [Header("Visuals")]
    [SerializeField] private GameObject ticker;
    [SerializeField] private float tick_interval;
    [SerializeField] private Effect_Explode explosion;
    private float tick_timer = 0;

    private void OnEnable()
    {
        ticking_down = false;
        detonation_timer = 0;
        ticker.SetActive(false);
        tick_timer = tick_interval;
    }

    private void Update()
    {

        if (!ticking_down)
        {
            foreach (var e in enemies)
            {
                float dist = Vector3.Distance(transform.position, e.transform.position);
                if (dist <= detonation_trigger_distance)
                {
                    ticking_down = true;
     
                    break;
                }
            }
            return;
        }

        Debug.Log("Ticking Down");
        tick_timer += Time.deltaTime;
        if (tick_timer > tick_interval * (1.1 - detonation_timer/detonation_delay))
        {
            ticker.SetActive(!ticker.activeSelf);
            tick_timer = 0;
        }


        detonation_timer += Time.deltaTime;

        if (detonation_timer < detonation_delay)
            return;

        foreach (var e in enemies)
        {
            e.transform.LookAt(transform.position);
            var rb = e.GetComponent<Rigidbody>();
            var knockback_dir = (e.transform.position - transform.position).normalized;
            knockback_dir.y = 0;

            if (!rb)
                continue;

            rb.velocity = Vector3.zero;
            rb.AddForce(knockback_dir * knockback_force, ForceMode.Impulse);

            e.GetComponent<HealthObject>().TakeDamage(explosion_damage, owner);

            /*
            var on_impact = e.gameObject.AddComponent<DoOnImpact>();
            e.enabled = false;

            on_impact.exclude_floor = true;
            on_impact.impact_event = () =>
            {
    
                e.enabled=true;
            };
            
            on_impact.tag_filter = new() { "Enemy", "Item" };
            */
        }
        Instantiate(explosion).transform.position = transform.position;
        
        gameObject.SetActive(false);

        Debug.Log("Kaboom");

    }

    private void OnTriggerEnter(Collider other)
    {
        var controller = other.GetComponent<EnemyController>();
        if (controller)
        {
            enemies.Add(controller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var controller = other.GetComponent<EnemyController>();
        if (controller && enemies.Contains(controller))
        {
            enemies.Remove(controller);
        }
    }


}
