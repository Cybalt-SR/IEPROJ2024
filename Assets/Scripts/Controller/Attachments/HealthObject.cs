using Assets.Scripts.Controller;
using Assets.Scripts.Controller.Attachments;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileHittable))]
public class HealthObject : MonoBehaviour
{
    [SerializeField] private int health;
    public int Health { get => health; }
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
}
