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
        private bool lerping2 = false;
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
                    StartCoroutine(Disappear());
                }
            }

            if (lerping2)
            {
                float t = 2.00f;
                float newX = Mathf.SmoothDamp(transform.localScale.x, 0.0f, ref vel, t);
                float newZ = Mathf.SmoothDamp(transform.localScale.z, 0.0f, ref vel, t);
                float newY = Mathf.SmoothDamp(transform.localScale.y, 0.0f, ref vel, t);
                this.transform.localScale = new Vector3(newX, newY, newZ);
                if (transform.localScale.x <= 0f || transform.localScale.y <= 0f || transform.localScale.z <= 0f)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(10);
            lerping2 = true;
        }

    }
}