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

    bool m_hook_launched = false;
    bool m_hook_planted = false;

    public override void Register()
    {
        OnOwnerSet = o =>{
            /*
            m_hook = AssetRequester.Instance.RequestComponent<Slime_Gun_Hook>(o.transform);
            if (!m_hook.gameObject.GetComponent<Rigidbody>())
                m_hook.gameObject.AddComponent<Rigidbody>();
            */
         
        };

        OnOwnerRemoving = o => {
            foreach(Transform ownable in m_ownable_assets)
            {

            }
            AssetRequester.Instance.DepositAsset("Slime Gun Hook", m_hook.gameObject);
        };
    
        base.Register();
    }

    public override async Task Cast()
    {
        base.Cast().GetAwaiter().GetResult();
        if (!m_hook_launched)
           await LaunchHook();   
        else if(m_hook_planted)
            await ReelGrappler();
    }

    public override void Update(float delta_time)
    {

    }

    #region Hook Subroutines
    private async Task LaunchHook()
    {
        float height_offset = Owner.GetComponent<Collider>().bounds.center.y;

    }

    private async Task ReelGrappler()
    {

    }
    #endregion


}
