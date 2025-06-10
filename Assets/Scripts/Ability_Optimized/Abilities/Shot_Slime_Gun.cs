using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using System.Threading.Tasks;
using Assets.Scripts.Controller;

public class Shot_Slime_Gun_data : AbilityData
{

    [Header("Assets")]
    public Material slime_material;

    [Header("Stats")]
    public float damage_multiplier;

    [Header("Shot Config")]
    public float projectile_speed;
    public float max_tether_distance;

    [Header("Grapple Config")]
    public float pull_force;
    public float grappling_multiplier;
    public float max_grapple_duration;
    public float impact_damage;

}

public class Shot_Slime_Gun : Ability
{

    private Material m_default_material;

    //private Slime_Gun_Adhesive m_adhesive;
    private Slime_Gun_Hook m_hook;

    public override void Register()
    {



        OnOwnerChanged = (old_owner, new_owner) => {
            if (!m_hook)
            {
                var slime_gun_hook_object = new GameObject();
                m_hook = slime_gun_hook_object.AddComponent<Slime_Gun_Hook>();
            }
            m_hook.transform.parent = new_owner.transform;
            m_hook.gameObject.SetActive(true);
        };

        OnOwnerRemoving = o => {
            m_hook.gameObject.SetActive(false);
        };
    
        base.Register();
    }



    public override async Task Cast()
    {
        await Task.Delay(0);
    }

    public override void Update(float delta_time)
    {

    }
}
