using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Mono.Cecil.Cil;

public class Slime_Gun_Adhesive : ProximityChecker
{
    [SerializeField] private float pulseForce = 5f;

    [Header("Stick Info")]
    [SerializeField] private Transform enemyParent = null;
    [SerializeField] private float stickOffset = 2f;

    [Header("Slimed Glow")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material slimeMaterial;



    private void OnDisable() => EjectEnemies();
    private void Start() => OnProximityEntered += (GameObject toStick) => SetStick(toStick, true);
    


    private void SetStick(GameObject toStick, bool value)
    {
        toStick.transform.SetParent(value ? PlayerController.GetFirst().transform : enemyParent);

        foreach (var item in toStick.GetComponents<MonoBehaviour>())
            item.enabled = !value;

        foreach (var item in toStick.GetComponentsInChildren<MonoBehaviour>())
            item.enabled = !value;

        var sr = toStick.GetComponentInChildren<SpriteRenderer>();
        sr.material = value ? slimeMaterial : defaultMaterial;

        var body = toStick.GetComponent<Rigidbody>();
        body.isKinematic = value;

        var coll = toStick.GetComponentInChildren<Collider>();
        coll.enabled = !value;

        var nav = toStick.GetComponent<NavMeshAgent>();
        nav.enabled = !value;

        var anim = toStick.GetComponentInChildren<Animator>();
        anim.enabled = !value;

        
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
    }

}
