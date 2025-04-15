using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class Slime_Gun_Adhesive : ProximityChecker
{
    [SerializeField] private float pulseForce = 5f;
    [SerializeField] private float maxStickDuration = 12f;

    [Header("Stick Info")]
    [SerializeField] private Transform enemyParent = null;

    [Header("Slimed Glow")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material slimeMaterial;


    private List<GameObject> temporaryBlacklist = new();

    private void OnDisable() => EjectEnemies();
    private void Start()
    {
        OnProximityEntered += (GameObject toStick) => {
            if (!temporaryBlacklist.Contains(toStick))
            {
                temporaryBlacklist.Add(toStick);
                SetStick(toStick, true);
            }          
        };
        OnProximityExit += (GameObject toStick) =>
        {
            if(temporaryBlacklist.Contains(toStick))
                temporaryBlacklist.Remove(toStick);
        };
    }

    private void Update()
    {

        //ermm
        List<GameObject> toRemove = new();

        foreach(var enemy in collisionList)
        {
            if (!enemy.GetComponent<EnemyStateHandler>().isStunned)
                toRemove.Add(enemy);
        }
           
        foreach(var enemy in toRemove)
            collisionList.Remove(enemy);

    }

    private void SetStick(GameObject toStick, bool value)
    {

        void AdditionalStunEffects(bool bvalue)
        {
            toStick.transform.parent = bvalue ? PlayerController.GetFirst().transform : enemyParent;

            var sr = toStick.GetComponentInChildren<SpriteRenderer>();
            sr.material = bvalue ? slimeMaterial : defaultMaterial;

            var body = toStick.GetComponent<Rigidbody>();
            body.isKinematic = bvalue;

            var coll = toStick.GetComponentInChildren<Collider>();
            coll.enabled = !bvalue;

            var nav = toStick.GetComponent<NavMeshAgent>();
            nav.enabled = !bvalue;
        }
        
        var EnemyStateHandler = toStick.GetComponent<EnemyStateHandler>();

        if (value)
            EnemyStateHandler.SetStun(maxStickDuration, AdditionalStunEffects);
        else EnemyStateHandler.RemoveDisables();

    }

    public void EjectEnemies()
    {
        foreach(var enemy in collisionList)
        {
            SetStick(enemy, false);
            var rb = enemy.GetComponent<Rigidbody>();
            var dir = enemy.transform.position - transform.position;
            rb.AddForce(pulseForce * dir.normalized, ForceMode.Impulse);
        }
        collisionList.Clear();
        temporaryBlacklist.Clear();
    }

}
