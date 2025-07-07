using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;

[CreateAssetMenu(fileName = "Ability_Circular_Gun_data", menuName = "Ability Optimized/Abilities/Circular Gun/Ability", order = 2)]
public class Ability_Circular_Gun_data : AbilityData
{
    [Header("Ability Stats")]
    public int max_spawnable_toys;

    [Header("Ballerina")]
    public GameObject ballerina;
    public float ballerina_lifetime;

    [Header("Soldier")]
    public GameObject soldier;
    public float soldier_lifetime;
    public float soldier_shot_damage;
    public int soldier_shots_per_second;

    [Header("Jack In A Box")]
    public GameObject jack_in_a_box;
    public float jack_detonation_delay;
    public float jack_detonation_trigger_distance;
    public float jack_knockback_force;
    public float jack_explosion_damage;

}
