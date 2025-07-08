using AbilityOP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shot_Slime_Gun", menuName = "Ability Optimized/Abilities/Slime Gun/Shot", order = 1)]
public class Shot_Slime_Gun_data : AbilityData
{

    [Header("Hook Config")]
    public float projectile_speed;
    public float max_tether_distance;
    public float lollipopping_multiplier;
    public float retract_speed;

    [Header("Grapple Config")]
    public float pull_force;
    public float anti_softlock_timer;

    [Header("Assets")]
    public Slime_Gun_Hook hook;


}