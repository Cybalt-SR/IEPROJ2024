using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public enum StatType
{
    DAMAGE,
    SHOTS_PER_SECOND,
    PROJECTILE_SPEED,
    RELOAD_TIME,
    PROJECTILES_PER_SHOT,
    CLIP_SIZE,
}

public class GunStatModifierHandler 
{
   
    private class StatMod
    {
        public float flat_effect;
        public float percent_effect;
        public float duration;

        public StatMod(float flat_effect, float percent_effect, float duration)
        {
            this.flat_effect = flat_effect;
            this.percent_effect = percent_effect;
            this.duration = duration;
        }
    }

    private Dictionary<StatType, Dictionary<string, StatMod> > handlers;


    public GunStatModifierHandler()
    {
        handlers = new();
        int size = Enum.GetNames(typeof(StatType)).Length;
        for (int i = 0; i < size; i++)
            handlers.Add((StatType)i, new());
    }

    public void AddMod(string name, StatType type, float flatAmount, float percentAmount, float duration = Mathf.Infinity)
    {
        if(!BuffExists(name, type))
        {
            var mod = new StatMod(flatAmount, percentAmount, duration);
            handlers[type].Add(name, mod);
        }
    }

    public void RemoveMod(string name, StatType type)
    {
        if (handlers[type].ContainsKey(name))
            handlers[type].Remove(name);
    }

    public void Update(float deltaTime)
    {

        foreach(var mod_handler in handlers) {
            List<string> removables = new();

            foreach(var mod in mod_handler.Value)
            {
                if(mod.Value.duration != Mathf.Infinity)
                    mod.Value.duration -= deltaTime;

                if (mod.Value.duration <= 0)
                    removables.Add(mod.Key);
            }

            foreach(var removable in removables)
                mod_handler.Value.Remove(removable);
            
        }
       
    }

    private float getTotal(float baseValue, StatType type)
    {
        Dictionary<string, StatMod> mods = handlers[type];
        float flat = 0;
        float percent = 0;

        foreach (var mod in mods)
        {
            flat += mod.Value.flat_effect;
            percent += mod.Value.percent_effect;
        }

        //base  + (base value * percent / 100) + flat
        float total = baseValue * (1 + (percent / 100)) + flat;

        return total;
    }
    public GunData ApplyStatMods(GunData baseGun)
    {
        baseGun.damage = (int)getTotal(baseGun.damage, StatType.DAMAGE);
        baseGun.shots_per_second = (int)getTotal(baseGun.shots_per_second, StatType.SHOTS_PER_SECOND);
        baseGun.projectile_speed = getTotal(baseGun.projectile_speed, StatType.PROJECTILE_SPEED);
        baseGun.reload_time = getTotal(baseGun.reload_time, StatType.RELOAD_TIME);
        baseGun.projectiles_per_shot = (int)getTotal(baseGun.projectiles_per_shot, StatType.PROJECTILES_PER_SHOT);
        baseGun.clip_size = (int)getTotal(baseGun.clip_size, StatType.CLIP_SIZE);

        return baseGun;
    }


    public void DebugStats()
    {
        string report = "";

        foreach(var handler in handlers)
        {
            float percentTotal = 0;
            float flatTotal = 0;
            foreach (var mod_handler in handler.Value)
            {
                percentTotal += mod_handler.Value.percent_effect;
                flatTotal += mod_handler.Value.flat_effect;
            }
            report += $"[{handler.Key.ToString()}]: [{percentTotal}%] [{flatTotal}]\n";
            
        }

        Debug.Log(report);
    }

    public bool BuffExists(string name, StatType type)
    {
        return handlers[type].ContainsKey(name);
    }
}
