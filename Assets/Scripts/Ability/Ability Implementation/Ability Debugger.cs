using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using gab_roadcasting;

using Abilities;

public class AbilityDebugger : Ability
{
    /*
        
        Sample Ability Name 

        Passive: Arbiter of Permanence
            >> Every time this ability is casted, gain 1 stack of count.

        Active: Something Something Pretentious Ability Name
            >> Print "Activated" for each stack of count accumulated.
    
     */

    private int count = 1;
    protected override void Cast()
    {
        for (int i = 0; i < count; i++)
            Debug.Log($"Activated. Count: {count}");
    }

    protected override void Initialize()
    {
        EventBroadcasting.AddListener(EventNames.COOLDOWN_EVENTS.ON_COOLDOWN_ENDED, (Dictionary<string, object> p) => count++);
    }
}
