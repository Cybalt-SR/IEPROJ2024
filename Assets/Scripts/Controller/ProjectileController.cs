using Assets.Scripts.Controller.Attachments;
using Assets.Scripts.Data;
using Assets.Scripts.Library;
using Assets.Scripts.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class ProjectileController : PooledBehaviour<ProjectileController>
    {
        private Rigidbody mRigidbody;
        private SphereCollider mSphereCollider;
        private TrailRenderer optional_trail;

        [SerializeField] private string ondeath_globaleffect_id;
        [SerializeField] private string onhit_globaleffect_id;
        [SerializeField] private string onbounce_globaleffect_id;

        public readonly Queue<Collider> ignore_list = new();

        private UnitController from = null;
        public GunData Data => from.Gun;
        private int bounce_count = 0;
        private int pierce_count = 0;
        private bool already_split = false;

        private Vector3 late_velocity;


        private void Awake()
        {
            mRigidbody = GetComponent<Rigidbody>();
            mSphereCollider = GetComponent<SphereCollider>();
            optional_trail = GetComponent<TrailRenderer>();
        }
        private void IgnoreCollider(Collider other)
        {
            ignore_list.Enqueue(other);
            Physics.IgnoreCollision(mSphereCollider, other);
        }

        private void ResetIgnores()
        {
            while (ignore_list.TryDequeue(out var ignoredCollider))
            {
                Physics.IgnoreCollision(mSphereCollider, ignoredCollider, false);
            }
            IgnoreCollider(from.Collider);
        }

        public void Shoot(Vector3 dir, UnitController from, ProjectileController parent = null, ProjectileHittable split_from = null)
        {
            this.from = from;
            ResetIgnores();

            if (split_from != null)
                IgnoreCollider(split_from.Collider);

            if (optional_trail)
                optional_trail.Clear();

            mRigidbody.velocity = dir * from.Gun.projectile_speed;

            bounce_count = 0;
            pierce_count = 0;
            already_split = false;

            if (parent == null)
                return;

            base.Assign(parent.Pool);
            bounce_count = parent.bounce_count;
            pierce_count = parent.pierce_count;
            already_split = true;
        }

        private void LateUpdate()
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
                projectileHittable.GetHit(this);

                if (pierce_count < from.Gun.pierce_count)
                {
                    IgnoreCollider(collision.collider);
                    mRigidbody.velocity = late_velocity;
                }

                pierce_count++;
                if (onhit_globaleffect_id != "")
                    GlobalEffectManager.Instance.Spawn(onhit_globaleffect_id, transform.position, collision.GetContact(0).normal);
            }
            else
            {
                bounce_count++;
                ResetIgnores();
                if (onbounce_globaleffect_id != "")
                    GlobalEffectManager.Instance.Spawn(onbounce_globaleffect_id, transform.position, collision.GetContact(0).normal);
            }

            if (already_split)
                Kill(projectileHittable);
            else if (pierce_count > from.Gun.pierce_count)
                Kill(projectileHittable);
            else if (bounce_count > from.Gun.bounce_count)
                Kill(projectileHittable);
        }

        private ProjectileController RequestOrDuplicate()
        {
            ProjectileController Duplicate()
            {
                var newobject = Instantiate(gameObject, ISingleton<PoolParent>.Instance.transform);
                return newobject.GetComponent<ProjectileController>();
            }

            ProjectileController toreturn;
            if (base.Pool.TryDequeue(out ProjectileController projectileController))
                toreturn = projectileController;
            else
                toreturn = Duplicate();

            toreturn.gameObject.SetActive(true);

            return toreturn;
        }

        private void Kill(ProjectileHittable hit = null)
        {
            if(ondeath_globaleffect_id != "")
                GlobalEffectManager.Instance.Spawn(ondeath_globaleffect_id, transform.position, late_velocity);

            gameObject.SetActive(false);

            if (already_split)
                return;

            var basis = mRigidbody.velocity.normalized;

            var starting_angle = 0;
            var quadrant_angle = 360.0f / from.Gun.split_count;
            var half_quadrant_angle = quadrant_angle / 2;

            for (int i = 0; i < from.Gun.split_count; i++)
            {
                var raw_angle = starting_angle + half_quadrant_angle + (i * quadrant_angle);
                var error_angle = (Random.value * from.Gun.error_angle) - (from.Gun.error_angle / 2);
                var final_dir = Quaternion.AngleAxis(raw_angle + error_angle, Vector3.up) * basis;

                var newprojectile = RequestOrDuplicate();

                newprojectile.transform.position = transform.position;

                newprojectile.Shoot(final_dir, from, parent: this, hit);
            }
        }
    }
}