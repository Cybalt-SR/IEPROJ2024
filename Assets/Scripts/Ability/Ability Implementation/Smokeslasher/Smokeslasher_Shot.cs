using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using System;
using gab_roadcasting;
using Assets.Scripts.Controller;
public class Smokeslasher_Shot : Ability
{

    [SerializeField] private float healthCost = 0.08f;

    [SerializeField] private TEMP_Attack_Anim attackRange;
    [SerializeField] private float damage_per_shot = 1f;


    protected override void Cast()
    {
        attackRange.gameObject.SetActive(true);
        IncurHealthCost();
    }


    private void AttackEffect(GameObject enemy)
    {
        var damage = new Wrapper<float>(damage_per_shot);
        var p = new Dictionary<string, object>();
        p.Add("Damage", damage);

        EventBroadcasting.InvokeEvent(EventNames.SECONDARY_EVENTS.ON_SECONDARY_SHOT, p);

        enemy.GetComponent<HealthObject>().TakeDamage(damage.value, "Player");
    }


    private void IncurHealthCost()
    {
        var player = PlayerController.GetFirst();
        var Health = player.GetComponent<OverloadHealthObject>();

        healthCost = Mathf.Clamp(healthCost, 0f, 1f);
        if (Health.Heat / Health.MaxHeat < 0.7f /*&& attackRange.CollisionList.Count > 0*/)
        {
            Action<Wrapper<float>> applyHealthCost = (hp) => hp.value += healthCost * Health.MaxHeat;
            Health.DoOnHealth(applyHealthCost);
        }
    }

    protected override void Initialize()
    {
        attackRange.OnProximityEntered += AttackEffect;
        //attackRange.OnFirstContact += IncurHealthCost;
        attackRange.gameObject.SetActive(false);
    }
}
