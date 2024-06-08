using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Game/Dictionary/Projectile")]
    public class ProjectileDictionary : SingletonResource<ProjectileDictionary>
    {
        public GameObject DefaultProjectile;
    }
}