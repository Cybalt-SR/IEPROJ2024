using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using System.Threading.Tasks;

public class SampleAbility_data : AbilityData { }

public class SampleAbility : Ability
{
    public override async Task Cast()
    {
        await Task.Delay(1000);
    }

}
