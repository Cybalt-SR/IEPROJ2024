using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityOP
{
    public class Hitbox : MonoBehaviour
    {
        private Collider m_collider;

        private List<GameObject> m_collisions = new();

        private void Awake()
        {
            m_collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            m_collisions.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            m_collisions.Remove(other.gameObject);
        }

        public List<GameObject> CaptureCollisions()
        {
            var collisions = new List<GameObject>(m_collisions);
            m_collisions.Clear();
            gameObject.SetActive(false);
            return collisions;
        }

    }
}

