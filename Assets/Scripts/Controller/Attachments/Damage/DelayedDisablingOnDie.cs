using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(HealthObject))]
    public class DelayedDisablingOnDie : MonoBehaviour
    {
        private HealthObject mHealthObject;
        [SerializeField] private float delay = 0.2f;

        private void Awake()
        {
            mHealthObject = GetComponent<HealthObject>();
        }

        private void Start()
        {
            mHealthObject.SubscribeOnDie(projectile =>
            {
                Invoke(nameof(DisableThis), delay);
            });
        }

        private void DisableThis()
        {
            this.gameObject.SetActive(false);
        }
    }
}