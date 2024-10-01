using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using System;
using gab_roadcasting;
public class Smokeslasher_Shot : Ability
{

    [SerializeField] private float healthCost = 0.08f;

    [SerializeField] private ProximityChecker attackRange;
    [SerializeField] private float damage_per_shot = 1f;


    protected override void Cast()
    {

        if (attackRange.CollisionList.Count == 0)
            return;

        var Health = owner.GetComponent<OverloadHealthObject>();

        healthCost = Mathf.Clamp(healthCost, 0f, 1f);

        if (Health.Heat / Health.MaxHeat < 0.7f)
        {
            Action<Wrapper<float>> applyHealthCost = (hp) => hp.value += healthCost * Health.MaxHeat;
            Health.DoOnHealth(applyHealthCost);
        }

        var damage = new Wrapper<float>(damage_per_shot);
        var p = new Dictionary<string, object>();
        p.Add("Damage", damage);

        EventBroadcasting.InvokeEvent(EventNames.SECONDARY_EVENTS.ON_SECONDARY_SHOT, p);

        foreach (var enemy in attackRange.CollisionList)
            enemy.GetComponent<HealthObject>().TakeDamage(damage.value, "Player");

    }

    protected override void Initialize()
    {}
}
