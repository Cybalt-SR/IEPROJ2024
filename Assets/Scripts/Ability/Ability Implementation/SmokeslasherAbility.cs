using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using gab_roadcasting;
using Assets.Scripts.Controller;

public class SmokeslasherAbility : Ability
{

    [Header("Passive: Decrease Overload")]
    [SerializeField] float overloadRestore = 0.7f;
    int enemiesKilled = 0;
    [SerializeField] int decreaseOnKillCount = 5;
    [SerializeField] float decreasePercent = 0.16f;

    [Header("Passive: Damage Amplification")]
    [SerializeField] float secondaryDamageMultiplier = 0.5f;
    [SerializeField] float primaryFirerateMultiplier = 0.5f;

    [Header("Active")]
    [SerializeField] float ability_damage = 5f;
    [SerializeField] private ProximityChecker areaOfEffect;

    protected override void Cast()
    {
        //Removes 70% of current overload
        var health = owner.GetComponent<OverloadHealthObject>();
        Action<Wrapper<float>> HalfOverload = (Wrapper<float> health) => health.value *= overloadRestore;
        health.DoOnHealth(HalfOverload);

        //Deals damage to all nearby enemies
        var damage = new Wrapper<float>(ability_damage);
        var p = new Dictionary<string, object>();
        p.Add("Damage", damage);

        EventBroadcasting.InvokeEvent(EventNames.SECONDARY_EVENTS.ON_SECONDARY_ABILITY, p);

        foreach (var enemy in areaOfEffect.CollisionList)
            enemy.GetComponent<HealthObject>().TakeDamage(damage.value, "Player");

        //Enter Invisibility


    }

    protected override void Initialize()
    {
    
        EventBroadcasting.AddListener(EventNames.ENEMY_EVENTS.ON_ENEMY_KILLED, OnEnemyKilled);
        EventBroadcasting.AddListener(EventNames.SECONDARY_EVENTS.ON_SECONDARY_SHOT, AmplifySecondaryDamage);
        EventBroadcasting.AddListener(EventNames.SECONDARY_EVENTS.ON_SECONDARY_ABILITY, AmplifySecondaryDamage);

        var Player = PlayerController.GetFirst();
        Player.AddFireRateMultiplier(gameObject.GetInstanceID() + AbilityData.AbilityID, AmplifyPrimaryFireRate);
    }

    private void OnDisable()
    {
        var Player = PlayerController.GetFirst();
        Player.RemoveFireRateMultiplier(gameObject.GetInstanceID() + AbilityData.AbilityID);
    }


    private void OnEnemyKilled(Dictionary<string, object> p)
    {
        var source = p["Source"] as string;

        if (source != "Player")
            return;

        if (++enemiesKilled % decreaseOnKillCount != 0)
        {
            var health = owner.GetComponent<OverloadHealthObject>();
            Action<Wrapper<float>> HalfOverload = (hp) => hp.value -= health.MaxHeat * decreasePercent;
            health.DoOnHealth(HalfOverload);
        }
    }


    private bool isHealthGreaterThanCertainPercentage(float hp)
    {
        hp = Mathf.Clamp(hp, 0f, 1f);
        var health = owner.GetComponent<OverloadHealthObject>();

        if (health.MaxHeat == 0)
            return false;

        return health.Heat / health.MaxHeat >= hp;
    }

    private void AmplifyPrimaryFireRate(Wrapper<float> shots_per_second)
    {
        if (isHealthGreaterThanCertainPercentage(0.4f))
            shots_per_second.value *= 1 + primaryFirerateMultiplier;
    }

    private void AmplifySecondaryDamage(Dictionary<string, object> p)
    {
        if (isHealthGreaterThanCertainPercentage(0.4f))
        {
            var wrapper = p["Damage"] as Wrapper<float>;
            Debug.Log($"Amplified! Original: {wrapper.value} New: {wrapper.value * (1f + secondaryDamageMultiplier)}");
            wrapper.value *= 1f + secondaryDamageMultiplier;
        }
    }




}
