using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(Collider))]
    public class ProjectileHittable : MonoBehaviour
    {
        private Collider mCollider;
        public Collider Collider { get { return mCollider; } }
        [SerializeField] private UnityEvent onHit;

        private void Awake()
        {
            mCollider = GetComponent<Collider>();
        }


        public void GetHit()
        {
            onHit.Invoke();
        }
    }
}