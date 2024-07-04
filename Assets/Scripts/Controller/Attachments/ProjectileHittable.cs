using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(Collider))]
    public class ProjectileHittable : MonoBehaviour
    {
        private Collider mCollider;
        public Collider Collider { get { return mCollider; } }
        private Action<ProjectileController> onHit;

        private void Awake()
        {
            mCollider = GetComponent<Collider>();
        }

        public void Subscribe(Action<ProjectileController> newaction)
        {
            onHit += newaction;
        }

        public void GetHit(ProjectileController projectile)
        {
            if (onHit == null)
                return;

            onHit.Invoke(projectile);
        }
    }
}