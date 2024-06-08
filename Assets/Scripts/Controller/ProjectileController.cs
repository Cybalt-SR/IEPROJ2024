using Assets.Scripts.Controller.Attachments;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileController : MonoBehaviour
    {
        private Rigidbody mRigidbody;

        [SerializeReference] private GunData from;
        [SerializeField] private int bounce_count = 0;
        [SerializeField] private int pierce_count = 0;

        private Vector3 late_velocity;

        private void Awake()
        {
            mRigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot(Vector3 dir, GunData from)
        {
            this.from = from;
            mRigidbody.velocity = dir * from.projectile_speed;
        }
        
        private void Update()
        {
            late_velocity = mRigidbody.velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var objectToCheck = collision.collider.gameObject;

            if(collision.rigidbody != null)
                objectToCheck = collision.rigidbody.gameObject;

            if(objectToCheck.TryGetComponent(out ProjectileHittable projectileHittable))
            {
                projectileHittable.GetHit();
                pierce_count++;
            }
            else
            {
                bounce_count++;
            }

            if (pierce_count >= from.pierce_count)
            {
                Kill();
                return;
            }
            if (bounce_count >= from.bounce_count)
            {
                Kill();
                return;
            }
        }

        private void Kill()
        {
            for (int i = 0; i < from.split_count; i++)
            {

            }

            this.gameObject.SetActive(false);
        }
    }
}