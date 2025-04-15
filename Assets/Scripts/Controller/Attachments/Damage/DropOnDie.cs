using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(HealthObject))]
    public class DropOnDie : MonoBehaviour
    {
        private HealthObject mHealthObject;

        [Serializable]
        private struct DropChance
        {
            public Pickup item;
            public float chance;
        }

        [SerializeField] private List<DropChance> drops;

        private void Awake()
        {
            mHealthObject = GetComponent<HealthObject>();
        }

        // Start is called before the first frame update
        void Start()
        {
            mHealthObject.SubscribeOnDie(projectile =>
            {
                foreach (var dropChance in drops)
                {
                    var chance = UnityEngine.Random.Range(0.0f, 100.0f);
                    chance /= 100;

                    if (chance > dropChance.chance)
                        continue;

                    PickupManager.CreatePickup(dropChance.item, transform.position);
                }
            });
        }
    }
}