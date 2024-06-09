using Assets.Scripts.Controller;
using UnityEngine;

namespace Assets.Scripts.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/Dictionary/Projectile")]
    public class ProjectileDictionary : SingletonDictionary<ProjectileController>
    {
    }
}