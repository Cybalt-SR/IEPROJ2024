using Assets.Scripts.Controller;
using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnUnitNear : MonoBehaviour
{
    [SerializeField] private List<GameObject> to_disable = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.gameObject.TryGetComponent(out UnitController player) == false)
            return;

        to_disable.ForEach(x => x.SetActive(false));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.gameObject.TryGetComponent(out UnitController player) == false)
            return;

        to_disable.ForEach(x => x.SetActive(true));
    }
}
