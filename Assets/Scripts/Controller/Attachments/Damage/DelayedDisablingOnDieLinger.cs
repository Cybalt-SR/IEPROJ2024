using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(HealthObject))]
    [RequireComponent(typeof(EnemyController))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class DelayedDisablingOnDieLinger : MonoBehaviour
    {
        private HealthObject mHealthObject;
        private EnemyController mEnemyController;
        private CapsuleCollider mCapsuleCollider;
        private Rigidbody mRigidbody;

        [SerializeField] private float delay = 0.2f;

        private bool lerping = false;
        float vel;

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
            lerping = true;
        }

        private void Update()
        {
            if (lerping)
            {
                float newY = Mathf.SmoothDamp(transform.localScale.y, 0.3f, ref vel, 0.1f);
                this.transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
                if(transform.localScale.y <= 0.305f)
                {
                    lerping = false;
                }
            }
        }

    }
}