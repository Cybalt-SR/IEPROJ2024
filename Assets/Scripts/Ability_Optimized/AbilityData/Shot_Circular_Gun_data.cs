using AbilityOP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Circular Gun Shot", menuName = "Ability Optimized/Abilities/Circular Gun/Shot", order = 1)]
public class Shot_Circular_Gun_data : AbilityData
{

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
