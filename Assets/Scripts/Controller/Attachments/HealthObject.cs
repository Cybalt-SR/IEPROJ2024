using Assets.Scripts.Controller;
using Assets.Scripts.Controller.Attachments;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileHittable))]
public class HealthObject : MonoBehaviour
{
    [SerializeField] private float health;
    public float Health { get => health; }
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
            health -= projectile.Data.damage;
            if (health <= 0 && onDie != null)
                onDie.Invoke(projectile);
            });
    }

    //temp

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) onDie?.Invoke(null);
    }

}
