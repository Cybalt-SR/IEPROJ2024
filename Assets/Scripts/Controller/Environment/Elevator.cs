using Assets.Scripts.Controller;
using Assets.Scripts.Data.ActionRequestTypes;
using Assets.Scripts.Library.ActionBroadcaster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private NextLevelActionRequest actionrequest;

    private void Awake()
    {
        if (actionrequest == null)
            throw new System.Exception("Action Request is null");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.gameObject.TryGetComponent(out PlayerController player) == false)
            return;

        if (player == null)
            return;

        ActionRequestManager.Request(actionrequest);
    }
}
