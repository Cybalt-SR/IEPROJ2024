using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;
using gab_roadcasting;
using Assets.Scripts.Controller;

public class Ability_Slime_Gun: Ability
{
    private Slime_Gun_Hook m_hook;
    private Slime_Gun_Grappler m_grappler;
    private Slime_Gun_Adhesive m_adhesive;

    private DetectionSphere m_pull_sphere;

    public override void Passive()
    {
        var ability_data = m_ability_data as Ability_Slime_Gun_data;
        void DamageReduction(Dictionary<string, object> p)
        {
            var damage = p["Damage"] as Wrapper<float>;

            float damageReductionRatio = m_hook.hook_planted ? ability_data.hooked_reduction_multiplier : ability_data.unhooked_reduction_multiplier;
            damageReductionRatio = Mathf.Min(damageReductionRatio, 100);

            damage.value *= 1 - (damageReductionRatio / 100);
        }

        m_passive_handler.TryAdd(EventNames.PLAYER_EVENTS.ON_OVERLOAD_CHANGED, DamageReduction);
    }


    protected override IEnumerator Active()
    {
        var ability_data = m_ability_data as Ability_Slime_Gun_data;

        foreach(var e in m_pull_sphere.Within)
        {
            var dir =  (Owner.transform.position - e.transform.position).normalized;
            var rb = e.GetComponent<Rigidbody>();
            var controller = e.GetComponent<UnitController>();

            controller.enabled = false;
            m_pull_sphere.StartCoroutine(DelayedControllerActivation(1f, controller));

            rb.AddForce(dir * ability_data.pull_force, ForceMode.Impulse);
        }

        var puddle_object = GameObject.Instantiate(ability_data.puddle.gameObject);
        var puddle = puddle_object.GetComponent<Slime_Gun_Puddle>();
        puddle.damage_over_time = ability_data.puddle_damage_over_time;
        puddle.dot_interval = ability_data.dot_tick_interval;
        puddle.puddle_lifetime = ability_data.puddle_lifetime;
        puddle.slow_power = ability_data.slow_power;
        puddle.target_scale = ability_data.expanded_scale;
        puddle.source = Owner.GetComponent<UnitController>();
        puddle.transform.position = Owner.transform.position;
        puddle.scale_speed = ability_data.expand_speed;
        puddle_object.SetActive(true);

        yield return null;
    }

    private void ConfigureAdhesive()
    {
        var ability_data = m_ability_data as Ability_Slime_Gun_data;

        m_hook = Owner.GetComponentInChildren<Slime_Gun_Hook>(true);
        m_grappler = Owner.GetComponent<Slime_Gun_Grappler>();

        var adhesive_object = GameObject.Instantiate(ability_data.adhesive.gameObject);
        m_adhesive = adhesive_object.GetComponent<Slime_Gun_Adhesive>();
        m_adhesive.sliming_distance = ability_data.sliming_distance;

        adhesive_object.transform.parent = Owner.transform;
        adhesive_object.transform.localPosition = new Vector3(0, -0.5f, 0);
        adhesive_object.SetActive(false);

        m_hook.on_hook_planted = () => adhesive_object.SetActive(true);
        m_hook.on_hook_retracted = () => m_adhesive.Unload(false);
        m_grappler.on_collision = () => m_adhesive.Unload(true);



        m_adhesive.on_detonation = enemies_trapped => {

            if (!Owner) return;

            var unload_dir = -Owner.transform.forward;
            unload_dir.y = 0;
            var unload_pos = Owner.transform.position + unload_dir * ability_data.unload_offset;
            foreach (var e in enemies_trapped)
            {
                if (e.GetComponent<BossController>() != null) return;

                var rb = e.GetComponent<Rigidbody>();
                var health = e.GetComponent<HealthObject>();

                Debug.Log("Kaboom");

                e.enabled = false;
                e.transform.position = unload_pos;
                health.TakeDamage(ability_data.impact_damage, Owner.GetComponent<UnitController>());
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.AddForce(unload_dir * ability_data.impact_force, ForceMode.Impulse);
         
                m_grappler.StartCoroutine(DelayedControllerActivation(0.4f, e));
            }
        };
    }

    protected override void OnOwnerSetting(GameObject owner)
    {
        base.OnOwnerSetting(owner);
        ConfigureAdhesive();
        var ability_data = m_ability_data as Ability_Slime_Gun_data;

        var pull_sphere_object = GameObject.Instantiate(ability_data.pull_sphere.gameObject);
        m_pull_sphere = pull_sphere_object.GetComponent<DetectionSphere>();
        m_pull_sphere.transform.parent = Owner.transform;
        m_pull_sphere.transform.localPosition = Vector3.zero;


    }

    protected override void OnOwnerRemoving(GameObject owner)
    {
        base.OnOwnerRemoving(owner);
        m_adhesive.Unload(false);
        GameObject.Destroy(m_adhesive.gameObject);
        GameObject.Destroy(m_pull_sphere.gameObject);   
    }

    IEnumerator DelayedControllerActivation(float delay, UnitController controller)
    {
        yield return new WaitForSeconds(delay);
        controller.enabled = true;
    }
}
