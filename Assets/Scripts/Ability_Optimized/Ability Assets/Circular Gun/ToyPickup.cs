using AbilityOP;
using Assets.Scripts.Controller.Attachments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyPickup : MonoBehaviour, IOnPlayerNear
{

    [HideInInspector] public float minimum_proximity_threshold = 1f;

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.one * 1.5f, ForceMode.Impulse);
    }

    public void OnPlayerNear(UnitController player)
    {
        var currently_equipped = SecondaryManager.Instance.currentlyEquipped;

        if (!currently_equipped || currently_equipped.secondary_name != "Circular Gun") 
            return;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if ( dist < minimum_proximity_threshold)
        {

            Transform toy_holder = player.transform.Find("Toy Holder");
            if (toy_holder != null)
            {
                transform.parent = toy_holder;
                gameObject.SetActive(false);
            }
   
        }

    }

}
