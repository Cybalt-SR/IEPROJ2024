using Assets.Scripts.Controller.Attachments;
using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using gab_roadcasting;

public class OverloadHealthObject : MonoBehaviour
{
    [SerializeField] private float heat;
    [SerializeField] private float max_heat;
    [SerializeField] private float cooldown_speed;
    public float Heat { get => heat; }
    public float MaxHeat { get => max_heat; }   
    private ProjectileHittable mProjectileHittable;

    [SerializeField] private UnityEvent<UnitController> onDie;

    [SerializeField] private UnityEvent<float> OnChangeHeatRatio = new();
    [SerializeField] private UnityEvent<float> OnChangeHeatRatio_reversed = new();

    public void SubscribeOnDie(Action<UnitController> newaction)
    {
        onDie.AddListener(newaction.Invoke);
    }

    private void Update()
    {
        heat -= Time.deltaTime * cooldown_speed;
        heat = MathF.Max(heat, 0);

        OnChangeHeatRatio.Invoke(heat / max_heat);
        OnChangeHeatRatio_reversed.Invoke(1.0f - (heat / max_heat));
    }

    public void Damage(float mdamage, UnitController from)
    {
        var damage = new Wrapper<float>(mdamage);

        var p = new Dictionary<string, object>();
        p.Add("Damage", damage);

        EventBroadcasting.InvokeEvent(EventNames.PLAYER_EVENTS.ON_OVERLOAD_CHANGED, p);

        heat += damage.value;
        if (heat >= max_heat && onDie != null)
            onDie.Invoke(from);
    }

    public void DoOnHealth(Action<Wrapper<float>> doer )
    {
        var wrapper = new Wrapper<float>(heat);
        doer?.Invoke(wrapper);
        heat = wrapper.value;
        heat = Mathf.Clamp(heat, 0, max_heat);
    }
}
