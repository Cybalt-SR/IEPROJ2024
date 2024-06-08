using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParent : MonoBehaviour, ISingleton<ProjectileParent>
{
    private void Awake()
    {
        ISingleton<ProjectileParent>.Instance = this;
    }
}
