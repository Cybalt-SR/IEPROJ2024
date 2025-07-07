using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutcracker_Circular_Gun : UnitController
{

    public float lifetime = 9f;
    public float shot_damage = 1f;
    public int shots_per_second = 2;

    private EnemyController currentTarget;
    private float time_active = 0;

    private DetectionSphere range_sphere;

    protected override void Start()
    {
        base.Start();
        range_sphere = GetComponentInChildren<DetectionSphere>();
    }

    protected override void Update()
    {
        base.Update();
        time_active += Time.deltaTime;

        if (time_active > lifetime)
        {
            gameObject.SetActive(false);
            return;
        }

        if(currentTarget == null || !currentTarget.gameObject.activeSelf)
        {
            var enemies = range_sphere.Within;
            if (enemies.Count == 0) return;
            currentTarget = enemies[UnityEngine.Random.Range(0, enemies.Count)].GetComponent<EnemyController>();
        }

        if (currentTarget == null || !currentTarget.gameObject.activeSelf)
            return;

        base.AimAt(currentTarget.transform.position);
        base.Fire();
    }


}
