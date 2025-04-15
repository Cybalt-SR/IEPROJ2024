using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Abilities;
public class SampleShot : Ability
{
    protected override void Cast()
    {
        Debug.Log("Shot!");
    }

    protected override void Initialize()
    {
   
    }


}
