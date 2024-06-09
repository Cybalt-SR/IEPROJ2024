using Assets.Scripts.Data.Pickup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(Collider))]
    public class PickupController : MonoBehaviour
    {
        [SerializeField] private Pickup pickup;

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {

        }
    }
}