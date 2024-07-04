using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(HealthObject))]
    public class DieEffectObject : MonoBehaviour
    {
        private HealthObject mHealthObject;

        [SerializeField] private string effect_id;

        private void Awake()
        {
            mHealthObject = GetComponent<HealthObject>();
        }

        private void Start()
        {
            mHealthObject.SubscribeOnDie(projectile =>
            {
                GlobalEffectManager.Instance.Spawn(effect_id, transform.position, Vector3.up);
                this.gameObject.SetActive(false);
            });
        }
    }
}