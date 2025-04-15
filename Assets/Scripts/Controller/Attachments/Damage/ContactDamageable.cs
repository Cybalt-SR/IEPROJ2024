using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(UnitController))]
public class ContactDamageable : MonoBehaviour
{
    private Collider mCollider;
    private UnitController mUnitController;
    public string Team => mUnitController == null ? "" : mUnitController.TeamId;

    public Collider Collider { get { return mCollider; } }
    [SerializeField] private UnityEvent<float, UnitController> onDamage;

    private void Awake()
    {
        mCollider = GetComponent<Collider>();
        mUnitController = GetComponent<UnitController>();
    }

    public void GetHit(ContactDamager doer)
    {
        if (onDamage == null)
            return;

        onDamage.Invoke(doer.GetDamage(), doer.From);
    }
}
