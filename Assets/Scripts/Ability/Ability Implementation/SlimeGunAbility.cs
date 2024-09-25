using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Abilities;
using gab_roadcasting;

public class SlimeGunAbility : Ability
{
    private bool isGrappling = false;

    [Header("Configurations")]
    [SerializeField] private float damageReductionPercent = 40f;
    [SerializeField] private float grapplingMultiplier = 2f;
    [SerializeField] private float knockStrength = 8f;


    [Header("References")]
    [SerializeField] private ProximityChecker proximityChecker;
    [SerializeField] private Puddle puddlePrefab;


    protected override void Cast()
    {

        Debug.Log("Casted Puddle");

        foreach (var enemy in proximityChecker.CollisionList)
        {
            var HealthObject = enemy.GetComponent<HealthObject>();
          
            Vector3 knockDirection = owner.transform.position-enemy.transform.position;
            knockDirection = Quaternion.Euler(0,0,45) * knockDirection.normalized;

            //scales knockStrength based on distance from the player
            float knockStrengthMultiplier = Vector3.Distance(enemy.transform.position, owner.transform.position) / proximityChecker.MaxDistance;
            knockStrengthMultiplier *= 2;

            var rb = enemy.GetComponent<Rigidbody>();
            rb.AddForce(knockDirection * knockStrengthMultiplier * knockStrength, ForceMode.Impulse);          
        }

        GameObject puddle = Instantiate(puddlePrefab.gameObject);

        Vector3 pos = owner.transform.position;
        pos.y = 0;
        puddle.transform.position = pos;



        proximityChecker.Clear();
    }

    protected override void Initialize()
    {
        EventBroadcasting.AddListener(EventNames.PLAYER_EVENTS.ON_OVERLOAD_CHANGED, DamageReduction);
    }

    private void DamageReduction(Dictionary<string, object> p)
    {
        var damage = p["Damage"] as Wrapper<float>;

        float damageReductionRatio = damageReductionPercent;
        if (isGrappling) 
            damageReductionRatio *= grapplingMultiplier;
        damageReductionRatio = Mathf.Clamp(damageReductionRatio, 0, 100);

        damage.value *= 1 - (damageReductionRatio / 100);

        Debug.Log("Damage Reduced");
    }


}
