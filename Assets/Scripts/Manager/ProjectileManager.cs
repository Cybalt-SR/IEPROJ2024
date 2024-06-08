using Assets.Scripts.Controller;
using Assets.Scripts.Data;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ProjectileManager : Manager_Base<ProjectileManager>
    {
        public void Shoot(Vector3 from, Vector3 dir, GunData gunData)
        {
            var newprojectile = Instantiate(ProjectileDictionary.Instance.DefaultProjectile, ISingleton<ProjectileParent>.Instance.transform);

            newprojectile.transform.position = from;
            newprojectile.GetComponent<ProjectileController>().Shoot(dir, gunData);
        }
    }
}