using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using gab_roadcasting;

[Serializable]
public class CooldownHandler
{

    [SerializeField] private string AbilityID;
    private Dictionary<string, object> parameter = new();

    [SerializeField] private float maxCooldown;
    [SerializeField] private float currentCooldown;

    public float MaxCooldown { get { return maxCooldown; } }
    public float CurrentCooldown { get { return currentCooldown; } }

   
    public CooldownHandler(float defaultCooldown, string abilityID)
    {

        Reset();

        parameter["Ability ID"] = AbilityID;
        this.maxCooldown = defaultCooldown;
  
    }

    public void Start()
    {
        currentCooldown = maxCooldown;
        FireCooldownEvent(EventNames.COOLDOWN_EVENTS.ON_COOLDOWN_BEGIN);
    }

    public void Reset()
    {
        currentCooldown = 0;
        FireCooldownEvent(EventNames.COOLDOWN_EVENTS.ON_COOLDOWN_RESET);
    }

    public void Update()
    {
        if (isCooldownFinished())
            return;

        currentCooldown -= Time.deltaTime;

        FireCooldownEvent(EventNames.COOLDOWN_EVENTS.ON_COOLDOWN_TICK);

        if(isCooldownFinished())
            FireCooldownEvent(EventNames.COOLDOWN_EVENTS.ON_COOLDOWN_ENDED);

    }

    public bool isCooldownFinished()
    {
        return currentCooldown <= 0;
    }

    private void FireCooldownEvent(string eventName)
    {
        parameter["Current Cooldown"] = currentCooldown;
        parameter["Max Cooldown"] = maxCooldown;

        EventBroadcasting.InvokeEvent(eventName, parameter);
    }

}
