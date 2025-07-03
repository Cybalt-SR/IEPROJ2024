using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(HealthObject))]
    [RequireComponent(typeof(EnemyController))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent (typeof(Rigidbody))]
    public class DelayedDisablingOnDieLinger : MonoBehaviour
    {
        private HealthObject mHealthObject;
        private EnemyController mEnemyController;
        private CapsuleCollider mCapsuleCollider;
        private Rigidbody mRigidbody;

        [SerializeField] private float delay = 0.2f;

        private void Awake()
        {
            mHealthObject = GetComponent<HealthObject>();
            mEnemyController = GetComponent<EnemyController>();
            mCapsuleCollider = GetComponent<CapsuleCollider>();
            mRigidbody = GetComponent<Rigidbody>();
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
            mEnemyController.enabled = false;
            mCapsuleCollider.enabled = false;
            mRigidbody.drag = 1;
        }
    }
}