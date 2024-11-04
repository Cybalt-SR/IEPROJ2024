using Assets.Scripts.Controller.Attachments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : UnitController, IOnPlayerNear, IOnLevelLoad
    {
        private NavMeshAgent mNavMesh;
        [SerializeField] private UnitController target;
        [SerializeField] private float maximum_distance = 6;
        [SerializeField] private float minimum_distance = 3;
        [SerializeField] private float lateral_randomness;
        [SerializeField] private float parallel_randomness;
        [SerializeField] private float random_tick_length;

        private Vector3 random_move_delta;
        private float time_since_last_randomized = 0;

        private Vector3 oldtarget_pos = Vector3.one * Mathf.Infinity;
        bool recalculate = true;

        private Vector3[] cachedcorners = null;


        protected override void Awake()
        {
            base.Awake();

            mNavMesh = GetComponent<NavMeshAgent>();
        }

        void IOnPlayerNear.OnPlayerNear(UnitController player)
        {
            target = player;

            var pStateHandler = player.GetComponent<PlayerStateHandler>();

            if (target == null || pStateHandler.isInvisible)
                return;

            base.AimAt(player.transform.position);
            var absoluteDelta = target.transform.position - this.transform.position;

            if ((target.transform.position - oldtarget_pos).sqrMagnitude > 0.25)
                recalculate = true;

            if (recalculate)
            {
                oldtarget_pos = target.transform.position;

                if (this.gameObject.activeInHierarchy == false)
                    return;
                if (mNavMesh.SetDestination(target.transform.position) == false)
                    return;

                cachedcorners = mNavMesh.path.corners;
            }

            if (cachedcorners.Length > 2)
            {
                var delta = cachedcorners[1] - this.transform.position;
                delta.y = 0;

                if(delta.sqrMagnitude < 0.25f)
                    recalculate = true;

                base.MoveDir = delta;
            }
            else {
                bool beyond_max = absoluteDelta.sqrMagnitude > maximum_distance * maximum_distance;
                bool within_min = absoluteDelta.sqrMagnitude < minimum_distance * minimum_distance;

                if (beyond_max && !within_min)
                    base.MoveDir = absoluteDelta;
                else
                {
                    if (within_min)
                        base.MoveDir = -absoluteDelta;

                    base.MoveDir = random_move_delta;
                }
            }
        }

        private void OnDrawGizmos()
        {

            if (cachedcorners == null || cachedcorners.Length == 0)
                return;

            Vector3 previous_pos = cachedcorners[0];

            foreach (var vertex in cachedcorners)
            {
                Gizmos.DrawLine(previous_pos, vertex);
                previous_pos = vertex;
            }
        }

        protected override void Update()
        {
            base.Update();

            if (target == null)
                return;

            time_since_last_randomized += Time.deltaTime;

            if(time_since_last_randomized > random_tick_length)
            {
                time_since_last_randomized = 0;
                var random_unit = Random.insideUnitCircle;

                var parallel_vec = target.transform.position - transform.position;
                parallel_vec.y = 0;
                parallel_vec = parallel_vec.normalized;
                var lateral_vec = Vector3.Cross(parallel_vec, Vector3.up);

                var random_lateral = random_unit.x * lateral_randomness * lateral_vec;
                var random_parallel = random_unit.y * parallel_randomness * parallel_vec;
                random_move_delta = random_lateral + random_parallel;
            }

            bool hit = Physics.Raycast(new Ray(this.transform.position, base.AimDir), out var info, 100);
            var target_distance = Vector3.SqrMagnitude(transform.position - target.transform.position);

            if(!hit) return;
            if ((info.distance * info.distance) > target_distance) return;
            
            if(info.collider.attachedRigidbody == null) return;
            if (info.collider.attachedRigidbody.gameObject != target.gameObject) return;

            base.Fire();
        }

        void IOnLevelLoad.OnLevelExit(GameObject curLevel)
        {
            Destroy(this.gameObject);
        }

        void IOnLevelLoad.OnLevelLoad(GameObject newLevel)
        {
        }
    }
}