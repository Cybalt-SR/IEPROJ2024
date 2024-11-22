using Assets.Scripts.Controller;
using Assets.Scripts.Controller.Attachments;
using gab_roadcasting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthObject : MonoBehaviour
{
    [SerializeField] private float health;
    public float Health { get => health; }

    [SerializeField] private UnityEvent additionalDieEvents;
    private Action<UnitController> onDie;
    public void SubscribeOnDie(Action<UnitController> newaction)
    {
        onDie += newaction;
    }

    //temp

    public void TakeDamage(float damage, UnitController source)
    {
        health -= damage;

        if (health > 0)
            return;
        
        additionalDieEvents?.Invoke();
        onDie?.Invoke(source);


        var p = new Dictionary<string, object>();
        p.Add("Enemy", gameObject);
        p.Add("Source", source);
        EventBroadcasting.InvokeEvent(EventNames.ENEMY_EVENTS.ON_ENEMY_KILLED, p);
    }

}
