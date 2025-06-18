using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AbilityOP
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Hitbox : MonoBehaviour
    {

        public IMediator<Hitbox> m_hitbox_mediator;

        protected SphereCollider m_collider;

        public List<string> m_filters = new();
        protected List<GameObject> m_collisions = new();

        public UnityEvent<GameObject> OnUnitDetected;
        public UnityEvent<GameObject> OnUnitRemoved;

        private void Awake()
        {
            m_collider = GetComponent<SphereCollider>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if(m_filters.Count == 0 || m_filters.Contains(other.tag))
            {
                m_collisions.Add(other.gameObject);
                OnUnitDetected?.Invoke(other.gameObject);
            }
               
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (m_filters.Count == 0 || m_filters.Contains(other.tag))
            {
                m_collisions.Remove(other.gameObject);
                OnUnitRemoved?.Invoke(other.gameObject);
            }
                
        }

        public virtual List<GameObject> CaptureCollisions()
        {
            var collisions = new List<GameObject>(m_collisions);
            m_collisions.Clear();
            m_filters.Clear();
            m_hitbox_mediator.Notify(Notification.HITBOX_NOTIFICATIONS.RETURN_TO_POOL, this);
            return collisions;
        }

        public void SetFilters(List<string> filters)
        {
            if(filters != null)
                m_filters = filters;
        }

    }
}

