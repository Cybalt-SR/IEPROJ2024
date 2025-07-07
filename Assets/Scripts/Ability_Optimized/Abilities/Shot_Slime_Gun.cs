using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using System.Threading.Tasks;
using Assets.Scripts.Controller;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;


public class Shot_Slime_Gun : Ability
{
    private bool force_terminate = false;


    private Slime_Gun_Hook m_hook;
    private Slime_Gun_Grappler m_grappler;


    protected override IEnumerator Active()
    {

        if (!m_hook.hook_planted)
        {
            if (!m_hook.hook_launched)
                LaunchHook();
        }
        else
        {
            yield return ReelGrappler();
        }

    }

    private void LaunchHook()
    {
        var ability_data = m_ability_data as Shot_Slime_Gun_data;
        var controller = Owner.GetComponent<UnitController>();

        m_hook.FireHook(controller.AimDir.normalized,  ability_data.projectile_speed, ability_data.max_tether_distance);
    }

    private IEnumerator ReelGrappler()
    {
        float soft_lock_timer = 0;

        var ability_data = m_ability_data as Shot_Slime_Gun_data;
        var rb = Owner.GetComponent<Rigidbody>();
        var agent = Owner.GetComponent<NavMeshAgent>();


        Debug.Log("Reeling");

        m_grappler.StartGrappling(m_hook.hook_plant_pos.Value, ability_data.pull_force);
    
        while (m_grappler.is_grappling && !force_terminate && soft_lock_timer < ability_data.anti_softlock_timer)
        {
            soft_lock_timer += Time.deltaTime;
            yield return null;
        }   
        
        //check if the grappler and hook are still active in case the player unequips midgrapple
        if(m_grappler && m_hook)
        {
            m_grappler.EndGrappling();
            m_hook.RetractHook();
        }
        
        Debug.Log("Ended Reeling");
    }

    protected override void OnOwnerSetting(GameObject owner)
    {
        base.OnOwnerSetting(owner);

        var ability_data = m_ability_data as Shot_Slime_Gun_data;
        var controller = Owner.GetComponent<UnitController>();

        var hook_object = GameObject.Instantiate(ability_data.hook.gameObject);
        m_hook = hook_object.GetComponent<Slime_Gun_Hook>();
        m_hook.transform.parent = Owner.transform;
        m_hook.gameObject.SetActive(false);

        m_hook.firing_reference = controller.ShootRef.transform;
        m_hook.lollipop_multiplier = ability_data.lollipopping_multiplier;
        m_hook.max_tether_distance = ability_data.max_tether_distance;
        m_hook.retract_speed = ability_data.retract_speed;

        m_grappler = Owner.AddComponent<Slime_Gun_Grappler>();
        force_terminate = false;
    }

    protected override void OnOwnerRemoving(GameObject owner)
    {
        base.OnOwnerRemoving(owner);
        GameObject.Destroy(m_hook.gameObject);
        force_terminate = false;
        Component.Destroy(m_grappler);
    }

}

