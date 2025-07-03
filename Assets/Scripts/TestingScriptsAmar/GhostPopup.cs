using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPopup : MonoBehaviour
{

    public GameObject EnemyPopup;
    public Light envLight;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.gameObject.TryGetComponent(out UnitController player) == false)
            return;
        envLight.intensity = 0.0f;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.gameObject.TryGetComponent(out UnitController player) == false)
            return;

        envLight.intensity = 100f;
        EnemyPopup.SetActive(true);
        
        this.gameObject.SetActive(false);
    }
}
