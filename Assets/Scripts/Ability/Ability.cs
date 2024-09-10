using System;
using System.Collections.Generic;
using UnityEngine;

using gab_roadcasting;

public abstract class Ability : MonoBehaviour
{
    
    [SerializeField] protected AbilityData abilityData;
    public AbilityData AbilityData { get { return abilityData; } }

    [Header("Cooldown Debugging")]
    [SerializeField] protected CooldownHandler cooldown;
    public CooldownHandler Cooldown { get { return cooldown; } }

    protected Dictionary<string, Action<Dictionary<string, object>> > passiveHandler = new();


    protected virtual void Update()
    {
        cooldown.Update();
    }

    public void Activate()
    {
        if (cooldown.isCooldownFinished())
        {
            Cast();
            cooldown.Start();
        }
    }

    protected virtual void Awake()
    {
        Initialize();

        foreach (var passive in passiveHandler)
            EventBroadcasting.AddListener(passive.Key, passive.Value);
       
        cooldown = new CooldownHandler(abilityData.Cooldown, abilityData.AbilityID);
    }

    protected abstract void Initialize();

    protected abstract void Cast();
}
