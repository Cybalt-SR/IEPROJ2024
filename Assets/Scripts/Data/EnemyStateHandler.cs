using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



//Currently only handles Stuns. Must be overhauled to accomodate other types of crowd control
//Mainly a state duration tracker like the player state manager

public class EnemyStateHandler : MonoBehaviour
{
    [SerializeField]private float StunDuration = 0f;
    private Action<bool> OnStunSet; //OnStunElapse

    public bool isStunned { get => StunDuration > 0f; }

    private void OnDisable()
    {
        OnStunSet?.Invoke(false);
    }

    public void SetStun(float duration, Action<bool> OnElapseAction = null)
    {
        StunDuration = Mathf.Max(StunDuration, duration);
        SetStunEffect(true);
        OnStunSet += OnElapseAction;
        OnElapseAction?.Invoke(true);
    }

    private void SetStunEffect(bool value)
    {
        foreach (var item in GetComponentsInChildren<MonoBehaviour>())
            if (item != this)
                item.enabled = !value;

        var anim = GetComponentInChildren<Animator>();
        anim.enabled = !value;
    }

    private void Update()
    {
        TickDownStun();
    }

    public void RemoveDisables()
    {
        StunDuration = Time.deltaTime;
    }

    private void TickDownStun()
    {
        if (StunDuration <= 0f)
            return;

        StunDuration -= Time.deltaTime;

        if (StunDuration <= 0f)
        {
            OnStunSet?.Invoke(false);
            OnStunSet = null;
            SetStunEffect(false);
      
        }
    }
}
