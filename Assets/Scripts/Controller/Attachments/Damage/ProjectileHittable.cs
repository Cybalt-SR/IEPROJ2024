using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(UnitController))]
    public class ProjectileHittable : MonoBehaviour
    {
        private Collider mCollider;
        private UnitController mUnitController;
        public string Team => mUnitController == null ? "" : mUnitController.TeamId;

        public Collider Collider { get { return mCollider; } }
        [SerializeField]private UnityEvent<ProjectileController> onHit;
        [SerializeField]private UnityEvent<float, UnitController> onDamage;

        private void Awake()
        {
            mCollider = GetComponent<Collider>();
            mUnitController = GetComponent<UnitController>();
        }

        public void Subscribe(Action<ProjectileController> newaction)
        {
            onHit.AddListener(newaction.Invoke);
        }

        public void GetHit(ProjectileController projectile)
        {
            if (onHit == null)
                return;

            onHit.Invoke(projectile);
            onDamage.Invoke(projectile.Data.damage, projectile.From);
        }
    }
}