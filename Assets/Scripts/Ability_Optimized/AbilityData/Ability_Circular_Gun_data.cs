using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;

[CreateAssetMenu(fileName = "Ability_Circular_Gun_data", menuName = "Ability Optimized/Abilities/Circular Gun/Ability", order = 2)]
public class Ability_Circular_Gun_data : AbilityData
{
    [Header("Ability Stats")]
    public int max_spawnable_toys;

    [Header("Assets")]
    public GameObject ballerina;
    public GameObject soldier;
    public GameObject jack_in_a_box;

}
