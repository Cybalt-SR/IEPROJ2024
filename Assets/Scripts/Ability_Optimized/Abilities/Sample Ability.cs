using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using System.Threading.Tasks;

[CreateAssetMenu(fileName ="Sample Ability", menuName = "Ability Optimized/Abilities/Sample/Sample1", order = 1)]
public class SampleAbility_data : AbilityData { }

public class SampleAbility : Ability
{
    public override async Task Cast()
    {
        await Task.Delay(1000);
        Debug.Log("Sample Ability Invoked");
    }

}
