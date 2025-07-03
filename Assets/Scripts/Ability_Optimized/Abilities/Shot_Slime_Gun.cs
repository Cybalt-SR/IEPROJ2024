using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using System.Threading.Tasks;
using Assets.Scripts.Controller;

[CreateAssetMenu(fileName = "Shot_Slime_Gun", menuName = "Ability Optimized/Abilities/Slime Gun/Shot", order = 1)]

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
/*
public class Shot_Slime_Gun : Ability
{

    private Material m_default_material;

    //private Slime_Gun_Adhesive m_adhesive;
    //private Slime_Gun_Line m_line;
    private Slime_Gun_Hook m_hook;

    private float m_height_offset;
    private float m_grapple_duration;
    private Vector3 m_aim_direction;
    private Vector3 m_delta_pos;

    private bool m_hook_launched = false;
    private bool m_hook_planted = false;
    private bool m_is_grappling = false;

 

    public override async Task Cast()
    {
        await base.Cast();

        if (!m_hook_planted)
        {
            if (!m_hook_launched)
                await LaunchHook();
        }
        else
        {
            await ReelGrappler();
        }
     
    }

    public override void Update(float delta_time)
    {
        if (!Owner) return;

        var ability_data = m_ability_data as Shot_Slime_Gun_data;

        var controller = Owner.GetComponent<UnitController>();
        m_aim_direction = controller.AimDir;

        if (m_is_grappling)
        {  
            m_delta_pos = m_hook.transform.position - Owner.transform.position;
            m_grapple_duration += Time.deltaTime;
            var rb = Owner.GetComponent<Rigidbody>();
            rb.AddForce(m_delta_pos.normalized * ability_data.pull_force * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (!m_is_grappling && Vector3.Distance(Owner.transform.position, m_hook.transform.position) > ability_data.max_tether_distance)
            EndGrapplerReeling();
    }

    #region  Owner Handling
    protected override void OnOwnerSetting(GameObject owner)
    {
        AssetRequester.LoadAsset<Slime_Gun_Hook>("Slime Gun Hook", out m_hook);
        m_height_offset = owner.GetComponent<Collider>().bounds.center.y;
        base.OnOwnerSetting(owner);
    }
    #endregion

    #region Hook Subroutines
    private async Task LaunchHook()
    {
        m_hook_launched = true;

        var ability_data = m_ability_data as Shot_Slime_Gun_data;
        m_hook.FireHook(m_aim_direction, ability_data.projectile_speed, 
            () => {
                m_hook_planted = true;
                m_hook_launched = false;
            }
        );

        await Task.Yield();
    }

    private async Task ReelGrappler()
    {

        var ability_data = m_ability_data as Shot_Slime_Gun_data;

        m_grapple_duration = 0;
        m_delta_pos = m_hook.transform.position - Owner.transform.position;
       

        SetGrapplingState(true);

        while (Vector3.SqrMagnitude(m_delta_pos) > 3f * 3f && m_grapple_duration < ability_data.max_grapple_duration)
        {   
            await Task.Delay(500);
        }

     
        SetGrapplingState(false);

        /*
         foreach (var enemy in adhesive.CollisionList)
         {
             var hp = enemy.GetComponent<HealthObject>();
             hp.enabled = true;
             hp.TakeDamage(impact_damage, player);
         }
   
        EndGrapplerReeling();

    }

    private void SetGrapplingState(bool value)
    {
        if (value == m_is_grappling) return;

        m_is_grappling = value;
        var controller = Owner.GetComponent<UnitController>();

        controller.Unit_State.can_move = !value;
        controller.Unit_State.can_attack = !value;

        Owner.gameObject.layer = LayerMask.NameToLayer(value ? "Ghost" : "Default");
    }

    private void EndGrapplerReeling()
    {
        SetGrapplingState(false);
        m_hook.gameObject.SetActive(false);
        m_hook.RetractHook();
        //disable adhesive here too
    }

    #endregion


}*/
