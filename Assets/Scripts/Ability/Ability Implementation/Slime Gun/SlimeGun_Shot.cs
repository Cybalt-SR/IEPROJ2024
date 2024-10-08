using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using Assets.Scripts.Controller;
using UnityEngine.AI;


public class SlimeGun_Shot : Ability
{
    [SerializeField] private GameObject hook;
    [SerializeField] private float projectileSpeed = 10f;


    [SerializeField] private float stickDuration;
    [SerializeField] private float max_grapple_duration = 8f;
    //EnemyTrap

    private PlayerController player;

    private void Start()
    {
        hook.SetActive(false);
        player = PlayerController.GetFirst();
    }
    protected override void Cast()
    {

        var hook_script = hook.GetComponent<SlimeGun_Hook>();

        if (!hook_script.isLocked)
            LaunchGrappler();
        else TriggerGrapple();

    }

    protected override void Initialize(){}

    private void SetGrappleState(bool value)
    {
        player.GetComponent<PlayerStateHandler>().canMove = !value;
        player.gameObject.layer = LayerMask.NameToLayer(value ? "Ghost" : "Default");
        player.GetComponent<NavMeshAgent>().isStopped = !value;

    }

    private void TriggerGrapple()
    {
        var hook_script = hook.GetComponent<SlimeGun_Hook>();
      
   

        IEnumerator Grapple()
        {

            float grapple_duration = 0f;
            var agent = player.GetComponent<NavMeshAgent>();

            SetGrappleState(true);
            
            float original_speed = agent.speed;
            float orinal_acceleration = agent.acceleration;

            agent.SetDestination(hook.transform.position);
            agent.speed = 20;
            agent.acceleration = 100;


            while(Vector3.Distance(hook.transform.position, player.transform.position) > 3f /*&& grapple_duration < max_grapple_duration*/)
            {
                grapple_duration += Time.deltaTime;
                yield return null;
           
            }

            agent.speed = original_speed;
            agent.acceleration = orinal_acceleration;

            hook.SetActive(false) ;
            hook_script.ClearLock();
            SetGrappleState(false);
        }
        StartCoroutine(Grapple());

        //disable movement
        //change player layer to ghost
        //pull player towards the hook
        //on arrival, deal damage to nearby enemies and stuns them
        //knock away attached enemies
    }

    private void LaunchGrappler()
    {
        var player = PlayerController.GetFirst();

        float heightOffset = player.GetComponent<Collider>().bounds.center.y;

        Vector3 aimDir = player.AimDir.normalized * 3;
        aimDir.y = 0.5f;

        Vector3 affixedPosition = player.transform.position + aimDir;
        affixedPosition.y += heightOffset;

        hook.transform.position = affixedPosition;
        hook.SetActive(true);

        var hookBody = hook.GetComponent<Rigidbody>();
        hookBody.AddForce(player.AimDir.normalized * projectileSpeed, ForceMode.Impulse);

    }

    private void OnDisable()
    {
        SetGrappleState(false);
    }



}
