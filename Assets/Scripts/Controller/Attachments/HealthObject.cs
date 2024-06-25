using Assets.Scripts.Controller.Attachments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileHittable))]
public class HealthObject : MonoBehaviour
{
    private ProjectileHittable mProjectileHittable;

    private void Awake()
    {
        mProjectileHittable = GetComponent<ProjectileHittable>();
    }


}
