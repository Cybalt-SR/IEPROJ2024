using Assets.Scripts.Controller.Attachments;
using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using gab_roadcasting;

[RequireComponent(typeof(ProjectileHittable))]
public class OverloadHealthObject : MonoBehaviour
{
    [SerializeField] private float heat;
    [SerializeField] private float max_heat;
    [SerializeField] private float cooldown_speed;
    public float Heat { get => heat; }
    private ProjectileHittable mProjectileHittable;

    [SerializeField] private UnityEvent<ProjectileController> onDie;

    [SerializeField] private UnityEvent<float> OnChangeHeatRatio = new();
    [SerializeField] private UnityEvent<float> OnChangeHeatRatio_reversed = new();


   

    public void SubscribeOnDie(Action<ProjectileController> newaction)
    {
        onDie.AddListener(newaction.Invoke);
    }
  

    private void Awake()
    {
        mProjectileHittable = GetComponent<ProjectileHittable>();

        mProjectileHittable.Subscribe(projectile => {

            var damage = new Wrapper<float>(projectile.Data.damage);

            var p = new Dictionary<string, object>();
            p.Add("Damage", damage);

            EventBroadcasting.InvokeEvent(EventNames.PLAYER_EVENTS.ON_OVERLOAD_CHANGED, p);

            heat += damage.value;
            if (heat >= max_heat && onDie != null)
                onDie.Invoke(projectile);

        });
    }

    private void Update()
    {
        heat -= Time.deltaTime * cooldown_speed;
        heat = MathF.Max(heat, 0);

        OnChangeHeatRatio.Invoke(heat / max_heat);
        OnChangeHeatRatio_reversed.Invoke(1.0f - (heat / max_heat));
    }
}
