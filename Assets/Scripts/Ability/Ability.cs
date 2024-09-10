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
    [SerializeField] protected string[] effectDependencies;

    protected void Awake()
    {
        foreach(var passive in passiveHandler)
            EventBroadcasting.AddListener(passive.Key, passive.Value);
       
        Initialize();

        cooldown = new CooldownHandler(abilityData.Cooldown, abilityData.AbilityID);
    }

    protected abstract void Initialize();

    public abstract void Cast();
}
