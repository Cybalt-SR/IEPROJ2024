using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using Assets.Scripts.Controller;

public class SlimeGun_Shot : Ability
{
    [SerializeField] private GameObject hook;
    [SerializeField] private float projectileSpeed;


    [SerializeField] private float stickDuration;
    //EnemyTrap

    private bool isGrappling { get => hook.activeInHierarchy; }


    protected override void Cast()
    {
        if (!isGrappling)
            LaunchGrappler();
        else TriggerGrapple();
    }

    private void TriggerGrapple()
    {
        //disable movement
        //change player layer to ghost
        //pull player towards the hook
        //on arrival, deal damage to nearby enemies and stuns them
        //knock away attached enemies
    }

    private void LaunchGrappler()
    {
        var player = PlayerController.GetFirst();
        hook.SetActive(true);

        var hookBody = hook.GetComponent<Rigidbody>();
        hookBody.AddForce(player.AimDir * projectileSpeed);

        //Enable EnemyTrap
        //SetHookLifetime

    }

    protected override void Initialize()
    {

    }
}
