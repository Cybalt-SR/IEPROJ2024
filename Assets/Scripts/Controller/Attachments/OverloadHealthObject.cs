using Assets.Scripts.Controller.Attachments;
using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileHittable))]
public class OverloadHealthObject : MonoBehaviour
{
    [SerializeField] private float heat;
    [SerializeField] private float max_heat;
    [SerializeField] private float cooldown_speed;
    public float Heat { get => heat; }
    private ProjectileHittable mProjectileHittable;

    private Action<ProjectileController> onDie;
    public void SubscribeOnDie(Action<ProjectileController> newaction)
    {
        onDie += newaction;
    }

    private void Awake()
    {
        mProjectileHittable = GetComponent<ProjectileHittable>();

        mProjectileHittable.Subscribe(projectile => {
            heat += projectile.Data.damage;
            if (heat >= max_heat && onDie != null)
                onDie.Invoke(projectile);
        });
    }

    private void Update()
    {
        heat -= Time.deltaTime * cooldown_speed;
        heat = MathF.Max(heat, 0);
    }
}
