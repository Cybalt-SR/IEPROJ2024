using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using System.Threading.Tasks;

[CreateAssetMenu(fileName ="Sample Ability", menuName = "Ability Optimized/Abilities/Sample/Sample 1", order = 1)]
public class SampleAbility_data : AbilityData { }

public class SampleAbility : Ability
{
    public override async Task Cast()
    {
        await base.Cast();
        Debug.Log("Sample Ability Invoked");
        await Task.Delay(5000);
        Debug.Log("Sample Ability Executed");

    }

}
