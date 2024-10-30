using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Abilities;
using gab_roadcasting;
using UnityEngine.AI;
using Assets.Scripts.Controller;

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

        IEnumerator tempStun(EnemyController enemy)
        {
            enemy.enabled = false;
            yield return new WaitForSeconds(stunDuration);
            enemy.enabled = true;
        }

        foreach (var enemy in proximityChecker.CollisionList)
        {
            //temp
            var ctrl = enemy.GetComponent<EnemyController>();
            puddle.GetComponent<MonoBehaviour>().StartCoroutine(tempStun(ctrl));
            puddle.GetComponent<MonoBehaviour>().StartCoroutine(ctrl.applySpeedModifier(10, 3));

            var HealthObject = enemy.GetComponent<HealthObject>();
          
            Vector3 knockDirection = owner.transform.position-enemy.transform.position;
            knockDirection = Quaternion.Euler(0,0,45) * knockDirection.normalized;

            //scales knockStrength based on distance from the player
            float knockStrengthMultiplier = Vector3.Distance(enemy.transform.position, owner.transform.position) / proximityChecker.MaxDistance;
            knockStrengthMultiplier *= 2;

            var rb = enemy.GetComponent<Rigidbody>();
            rb.AddForce(knockDirection * knockStrengthMultiplier * knockStrength, ForceMode.Impulse);
        
        }



        proximityChecker.Clear();
    }

    protected override void Initialize(){}



}
