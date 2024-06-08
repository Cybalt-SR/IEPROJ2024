using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Controller.Attachments
{
    public class ProjectileHittable : MonoBehaviour
    {
        [SerializeField] private UnityEvent onHit;

        public void GetHit()
        {
            onHit.Invoke();
        }
    }
}