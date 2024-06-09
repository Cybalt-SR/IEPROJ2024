using Assets.Scripts.Controller;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ProjectileManager : PoolManager<ProjectileManager, ProjectileController>
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
                var error_angle = (Random.value * gunData.error_angle) - (gunData.error_angle / 2);
                var final_dir = Quaternion.AngleAxis(raw_angle + error_angle, Vector3.up) * dir;

                var newprojectile = RequestOrCreate(gunData.projectile_id);

                newprojectile.transform.position = from;

                newprojectile.Shoot(final_dir, shooter);
            }
        }
    }
}