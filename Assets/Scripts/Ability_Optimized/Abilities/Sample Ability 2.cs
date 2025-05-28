using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using AbilityOP;

public class SampleAbility2_data: AbilityData
{
    public float some_value;
}
public class SampleAbility2 : Ability
{

    public override async Task Cast()
    {
        await Task.Delay(1000);
        Debug.Log("Sample Ability 2");
    }
}

