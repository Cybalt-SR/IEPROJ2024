using AbilityOP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Slime_Gun", menuName = "Ability Optimized/Abilities/Slime Gun/Ability", order = 2)]
public class Ability_Slime_Gun_data : AbilityData
{
    [Header("Stats")]
    public float impact_damage;
    public float impact_force;

    [Header("Passive")]
    public float unhooked_reduction_multiplier;
    public float hooked_reduction_multiplier;

    [Header("Active")]
    public float pull_force;

    public float puddle_damage_over_time;
    public float dot_tick_interval;
    public float puddle_lifetime;
    public float slow_power;
    public Vector3 expanded_scale;
    public float expand_speed;

    [Header("Config")]
    public float sliming_distance;
    public float unload_offset;

    [Header("Assets")]
    public Slime_Gun_Adhesive adhesive;
    public DetectionSphere pull_sphere;
    public Slime_Gun_Puddle puddle;
}
