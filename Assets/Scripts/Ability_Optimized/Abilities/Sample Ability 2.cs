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

    float updatable = 0;
    bool used = false;
    protected override IEnumerator Active()
    {
        var data = this.m_ability_data as SampleAbility2_data;
        Debug.Log($"Used: {updatable}");
        Debug.Log("Sample Ability 2");
        Debug.Log($"Some Value: {data.some_value}");
        used = true;
        yield return null;
    }

    public override void Update(float deltaTime)
    {
        if(used)
            updatable++;
    }

}

