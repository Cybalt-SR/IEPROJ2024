using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(Collider))]
    public class PickupController : MonoBehaviour
    {
        [SerializeField] private Pickup pickup;
        public Pickup Pickup { get { return pickup; } }

        public void SetPickup(Pickup pickup)
        {
            this.pickup = pickup;
        }

        public void PickedUp()
        {
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger)
                return;

            if (other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.gameObject.TryGetComponent(out PlayerController player) == false)
                return;

            PickupManager.SetNearby(player, this, true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.isTrigger)
                return;

            if (other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.gameObject.TryGetComponent(out PlayerController player) == false)
                return;

            PickupManager.SetNearby(player, this, false);
        }
    }
}