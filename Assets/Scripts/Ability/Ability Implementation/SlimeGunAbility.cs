using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Abilities;
using gab_roadcasting;

public class SlimeGunAbility : Ability
{
    private bool isGrappling = false;
    [SerializeField] private float damageReductionPercent = 40f;
    [SerializeField] private float grapplingMultiplier = 2f;

    [SerializeField] private ProximityChecker proximityChecker;
    protected override void Cast()
    {
        Debug.Log(proximityChecker.CollisionList);
    }

    protected override void Initialize()
    {
        EventBroadcasting.AddListener(EventNames.PLAYER_EVENTS.ON_OVERLOAD_CHANGED, DamageReduction);
    }

    private void DamageReduction(Dictionary<string, object> p)
    {
        var damage = p["Damage"] as Wrapper<float>;

        float damageReductionRatio = damageReductionPercent;
        if (isGrappling) 
            damageReductionRatio *= grapplingMultiplier;
        damageReductionRatio = Mathf.Clamp(damageReductionRatio, 0, 100);

        damage.value *= 1 - (damageReductionRatio / 100);

        Debug.Log("Damage Reduced");
    }


}
