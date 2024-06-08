using Assets.Scripts.Controller.Attachments;
using Assets.Scripts.Data;
using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class ProjectileController : MonoBehaviour
    {
        private Rigidbody mRigidbody;
        private SphereCollider mSphereCollider;
        public SphereCollider Collider { get { return mSphereCollider; } }

        [SerializeReference] private UnitController from;
        [SerializeField] private int bounce_count = 0;
        [SerializeField] private int pierce_count = 0;
        [SerializeField] private bool already_split = false;

        private Vector3 late_velocity;

        private void Awake()
        {
            mRigidbody = GetComponent<Rigidbody>();
            mSphereCollider = GetComponent<SphereCollider>();
        }

        public void Shoot(Vector3 dir, UnitController from)
        {
            this.from = from;
            mRigidbody.velocity = dir * from.Gun.projectile_speed;
        }

        private void Update()
        {
            late_velocity = mRigidbody.velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var objectToCheck = collision.collider.gameObject;

            if (collision.rigidbody != null)
                objectToCheck = collision.rigidbody.gameObject;

            if (objectToCheck.TryGetComponent(out ProjectileHittable projectileHittable))
            {
                if (pierce_count < from.Gun.pierce_count)
                {
                    Physics.IgnoreCollision(mSphereCollider, collision.collider);
                    mRigidbody.velocity = late_velocity;
                }

                projectileHittable.GetHit();
                pierce_count++;
            }
            else
            {
                bounce_count++;
            }

            if (pierce_count > from.Gun.pierce_count)
            {
                Kill();
                return;
            }
            if (bounce_count > from.Gun.bounce_count)
            {
                Kill();
                return;
            }
        }

        private void Kill()
        {
            this.gameObject.SetActive(false);

            if (already_split)
                return;

            var basis = mRigidbody.velocity.normalized;

            var starting_angle = 0;
            var quadrant_angle = 360.0f / from.Gun.split_count;
            var half_quadrant_angle = quadrant_angle / 2;

            for (int i = 0; i < from.Gun.split_count; i++)
            {
                var raw_angle = starting_angle + half_quadrant_angle + (i * quadrant_angle);

                var newprojectile = Instantiate(this, ISingleton<ProjectileParent>.Instance.transform);

                newprojectile.gameObject.SetActive(true);
                newprojectile.transform.position = this.transform.position;

                var error_angle = (Random.value * from.Gun.error_angle) - (from.Gun.error_angle / 2);
                var final_dir = Quaternion.AngleAxis(raw_angle + error_angle, Vector3.up) * basis;
                var projectileController = newprojectile.GetComponent<ProjectileController>();
                projectileController.Shoot(final_dir, from);
                projectileController.already_split = true;
                Physics.IgnoreCollision(from.Collider, projectileController.Collider);
            }
        }
    }
}