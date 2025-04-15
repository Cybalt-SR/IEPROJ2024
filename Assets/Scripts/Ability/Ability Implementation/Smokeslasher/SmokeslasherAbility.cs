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

    [SerializeField] int decreaseOnKillCount = 5;
    [SerializeField] float decreasePercent = 0.16f;

    [Header("Passive: Damage Amplification")]
    //[SerializeField] float primaryFirerateMultiplier = 300f;

    [Header("Active")]
    [SerializeField] private TEMP_Attack_Anim areaOfEffect;
    [SerializeField] private float invisbilityDuration = 3f;

    int enemiesKilled = 0;
    private string instanceID;


    protected override void Cast()
    {
        //Removes 70% of current overload
        var health = owner.GetComponent<OverloadHealthObject>();
        Action<Wrapper<float>> HalfOverload = (Wrapper<float> health) => health.value -= health.value * overloadRestore;
        health.DoOnHealth(HalfOverload);

        //Enter Invisibility
        areaOfEffect.gameObject.SetActive(true);
    }


    protected override void Initialize()
    {
        EventBroadcasting.AddListener(EventNames.ENEMY_EVENTS.ON_ENEMY_KILLED, OnEnemyKilled);
        instanceID = gameObject.GetInstanceID().ToString();

        areaOfEffect.OnEnd += () =>
        {
            var stateManager = owner.GetComponent<PlayerStateHandler>();
            stateManager.InvokeInvisibility(invisbilityDuration);
        };

    }

    private void OnEnable()
    {
        var Player = PlayerController.GetFirst();

        Player.StatModder.AddMod(instanceID, StatType.PROJECTILES_PER_SHOT, 2f, 0f);
        areaOfEffect.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        var Player = PlayerController.GetFirst();

        if (Player != null)
        {
            Player.StatModder.RemoveMod(instanceID, StatType.PROJECTILES_PER_SHOT);
            Player.StatModder.RemoveMod(instanceID, StatType.SHOTS_PER_SECOND);
            Player.StatModder.RemoveMod(instanceID, StatType.RELOAD_TIME);
        }
    }

    private void OnEnemyKilled(Dictionary<string, object> p)
    {
        var source = p["Source"] as string;

        if (source != "Player")
            return;

        if (++enemiesKilled % decreaseOnKillCount == 0)
        {
            Debug.Log($"Restoring HP after {decreaseOnKillCount} kills");
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

    protected override void Update()
    {
        base.Update();

        var Player = PlayerController.GetFirst();
        if (isHealthGreaterThanCertainPercentage(0.4f))
        {
            Player.StatModder.AddMod(instanceID, StatType.SHOTS_PER_SECOND, 0, 300);
            Player.StatModder.AddMod(instanceID, StatType.RELOAD_TIME, 0, -300);
        }
        else
        {
            Player.StatModder.RemoveMod(instanceID, StatType.SHOTS_PER_SECOND);
            Player.StatModder.RemoveMod(instanceID, StatType.RELOAD_TIME);
        }
    }






}
