using Assets.Scripts.Controller;
using Assets.Scripts.Data.AssetDictionaries;
using UnityEngine;

namespace Assets.Scripts.Data.AssetDictionaries
{
    [CreateAssetMenu(menuName = "Game/Dictionary/Projectile")]
    public class ProjectileDictionary : SingletonDictionary<ProjectileController>
    {
    }
}