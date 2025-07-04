using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using gab_roadcasting;
using System.Runtime.CompilerServices;
using System;

[CreateAssetMenu(fileName = "Circular Gun Shot", menuName = "Ability Optimized/Abilities/Circular Gun/Shot", order = 1)]
public class Shot_Circular_Gun_data : AbilityData {

    [Header("Gun Stats")]
    public float projectile_damage;
    public float projectile_speed;
    public float toy_spawn_rate;
    public float maximum_toy_spawn_distance;
    public int maximum_extra_projectiles;
    public int maximum_projectile_bounce;


    [Header("Config")]
    public float toy_spawn_height_offset;
    public float minimum_toy_collect_proximity;
    public float spread_angle;
    public float firing_reference_offset;

    [Header("Assets")]
    public GameObject toy_pickup;
    public GameObject projectile;

}

public class Shot_Circular_Gun : Ability
{

    private Transform m_toy_holder;

    public override void Passive()
    {
        var ability_data = m_ability_data as Shot_Circular_Gun_data;

        //Create Toy Pickups on Nearby Death
        Action<Dictionary<string, object>> OnEnemyDeath =
            p => {
                Vector3 pos = (p["Enemy"] as GameObject).transform.position;

                float dist = Vector3.Distance(Owner.transform.position, pos);
                if (dist < ability_data.maximum_toy_spawn_distance)
                {
                    var toy_pickup = GameObject.Instantiate(ability_data.toy_pickup);
                    pos.y += ability_data.toy_spawn_height_offset;
                    toy_pickup.transform.position = pos;
                    toy_pickup.GetComponent<ToyPickup>().minimum_proximity_threshold = ability_data.minimum_toy_collect_proximity;
                }
            };


        m_passive_handler.TryAdd(EventNames.ENEMY_EVENTS.ON_ENEMY_KILLED, OnEnemyDeath);
    }

    protected override IEnumerator Active()
    {
        var controller = Owner.GetComponent<UnitController>();
        var ability_data = m_ability_data as Shot_Circular_Gun_data;

        Vector3 normal = controller.AimDir.normalized;
        Vector3 spawn_pos = controller.ShootRef.position + normal * ability_data.firing_reference_offset;

        int projectile_count = 1 + Mathf.Min(ability_data.maximum_extra_projectiles, m_toy_holder.childCount);

        var starting_angle = -ability_data.spread_angle / 2;
        var quadrant_angle = ability_data.spread_angle / projectile_count;
        var half_quadrant_angle = quadrant_angle / 2;

        for (int i = 0; i < projectile_count; i++)
        {
            var raw_angle = starting_angle + half_quadrant_angle + (i * quadrant_angle);
            var final_dir = Quaternion.AngleAxis(raw_angle, Vector3.up) * normal;

            var disc = GameObject.Instantiate(ability_data.projectile);
            disc.transform.position = spawn_pos;

            var proj = disc.GetComponent<Projectile_CircularGun>();
            proj.max_bounces = ability_data.maximum_projectile_bounce;
            proj.bullet_damage = ability_data.projectile_damage;
            proj.Shoot(final_dir, ability_data.projectile_speed);
        }
       
        foreach (Transform t in m_toy_holder)
        {
            GameObject.Destroy( t.gameObject);
        }
        yield return null;
    }

 

    protected override void OnOwnerSetting(GameObject owner)
    {
        base.OnOwnerSetting(owner);

        var ability_data = m_ability_data as Shot_Circular_Gun_data;

        //Look for Toy Holder Transform under Owner, if it's not there, create it

        if (!Owner.transform.Find("Toy Holder"))
        {
            GameObject toy_holder = new GameObject("Toy Holder");
            toy_holder.transform.parent = Owner.transform;
            m_ownable_assets.Add(toy_holder.transform);
        }
     
        m_toy_holder = Owner.transform.Find("Toy Holder");
       
    }

    
}