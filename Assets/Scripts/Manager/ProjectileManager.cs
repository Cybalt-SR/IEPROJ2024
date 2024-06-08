using Assets.Scripts.Controller;
using Assets.Scripts.Data;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ProjectileManager : Manager_Base<ProjectileManager>
    {
        public void Shoot(Vector3 from, Vector3 dir, UnitController shooter)
        {
            var gunData = shooter.Gun;

            var starting_angle = -gunData.spread_angle / 2;
            var quadrant_angle = gunData.spread_angle / gunData.projectiles_per_shot;
            var half_quadrant_angle = quadrant_angle / 2;

            for (int i = 0; i < gunData.projectiles_per_shot; i++)
            {
                var raw_angle = starting_angle + half_quadrant_angle + (i * quadrant_angle);

                var newprojectile = Instantiate(ProjectileDictionary.Instance.DefaultProjectile, ISingleton<ProjectileParent>.Instance.transform);

                newprojectile.transform.position = from;

                var error_angle = (Random.value * gunData.error_angle) - (gunData.error_angle / 2);
                var final_dir = Quaternion.AngleAxis(raw_angle + error_angle, Vector3.up) * dir;
                var projectileController = newprojectile.GetComponent<ProjectileController>();
                projectileController.Shoot(final_dir, shooter);
                Physics.IgnoreCollision(projectileController.Collider, shooter.Collider);
            }
        }
    }
}