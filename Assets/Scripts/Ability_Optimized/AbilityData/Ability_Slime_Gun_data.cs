using AbilityOP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Slime_Gun", menuName = "Ability Optimized/Abilities/Slime Gun/Ability", order = 2)]
public class Ability_Slime_Gun_data : AbilityData
{
    [Header("Stats")]
    public float impact_damage;
    public float damage_multiplier;
    public float grappling_multiplier;

    [Header("Assets")]
    public Slime_Gun_Adhesive adhesive;
}
