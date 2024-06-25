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
        [SerializeField] private Action<ProjectileController> onHit;

        private void Awake()
        {
            mCollider = GetComponent<Collider>();
        }


        public void GetHit(ProjectileController projectile)
        {
            onHit.Invoke(projectile);
        }
    }
}