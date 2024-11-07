using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Abilities;
using gab_roadcasting;
using UnityEngine.AI;
using Assets.Scripts.Controller;
using UnityEngine.InputSystem;

public class SlimeGunAbility : Ability
{

    [Header("Configurations")]
    [SerializeField] private float knockStrength = 8f;
    [SerializeField] private float stunDuration = .5f;


    [Header("References")]
    [SerializeField] private ProximityChecker proximityChecker;
    [SerializeField] private Puddle puddlePrefab;


    protected override void Cast()
    {


        Debug.Log("Casted Puddle");


        GameObject puddle = Instantiate(puddlePrefab.gameObject);

        Vector3 pos = owner.transform.position;
        pos.y = 0;
        puddle.transform.position = pos;


        foreach (var enemy in proximityChecker.CollisionList)
        {
            if(!enemy.GetComponent<EnemyController>().enabled)
                continue;
     
            var HealthObject = enemy.GetComponent<HealthObject>();
          
            Vector3 knockDirection = owner.transform.position-enemy.transform.position;
            knockDirection = Quaternion.Euler(0,0,45) * knockDirection.normalized;

            //scales knockStrength based on distance from the player
            float knockStrengthMultiplier = Vector3.Distance(enemy.transform.position, owner.transform.position) / proximityChecker.MaxDistance;
            knockStrengthMultiplier *= 2;

            var rb = enemy.GetComponent<Rigidbody>();
            rb.AddForce(knockDirection * knockStrengthMultiplier * knockStrength, ForceMode.Impulse);

            enemy.GetComponent<EnemyStateHandler>().SetStun(stunDuration);

        }



        proximityChecker.Clear();
    }

    protected override void Initialize(){}



}
