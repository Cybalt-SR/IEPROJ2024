using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using AbilityOP;

[CreateAssetMenu(fileName = "SampleAbility2", menuName = "Ability Optimized/Abilities/Sample/Sample 2", order = 1)]
public class SampleAbility2_data: AbilityData
{
    public float some_value;
}
public class SampleAbility2 : Ability
{
    public override async Task Cast()
    {
        var data = this.m_ability_data as SampleAbility2_data;

        Debug.Log("Sample Ability 2");
        Debug.Log($"Some Value: {data.some_value}");
    }
}

