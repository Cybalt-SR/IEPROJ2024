using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;
using System.Threading.Tasks;

[CreateAssetMenu(fileName ="Sample Ability", menuName = "Ability Optimized/Abilities/Sample/Sample 1", order = 1)]
public class SampleAbility_data : AbilityData { }

public class SampleAbility : Ability
{
    protected override IEnumerator Active()
    {
        Debug.Log("Sample Ability Invoked");

        Resources.Load("The Debug Cube");

        yield return new WaitForSeconds(5);
        Debug.Log("Sample Ability Executed");

    }

}
