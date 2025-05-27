using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityOP
{
    public class Hitbox : MonoBehaviour
    {
        private Collider m_collider;
        public IMediator<Hitbox> m_hitbox_mediator;
        public List<string> m_filters = new();

        private List<GameObject> m_collisions = new();

        private void Awake()
        {
            m_collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(m_filters.Count == 0 || m_filters.Contains(other.tag))
                m_collisions.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (m_filters.Count == 0 || m_filters.Contains(other.tag))
                m_collisions.Remove(other.gameObject);
        }

        
        public List<GameObject> CaptureCollisions()
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

