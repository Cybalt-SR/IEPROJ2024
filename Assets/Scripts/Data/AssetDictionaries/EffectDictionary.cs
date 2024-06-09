using Assets.Scripts.Controller;
using Assets.Scripts.Data.AssetDictionaries;
using UnityEngine;

namespace Assets.Scripts.Data.AssetDictionaries
{
    [CreateAssetMenu(menuName = "Game/Dictionary/Effect")]
    public class EffectDictionary : SingletonDictionary<EffectController>
    {
    }
}