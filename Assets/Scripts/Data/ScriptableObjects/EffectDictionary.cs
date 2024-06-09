using Assets.Scripts.Controller;
using UnityEngine;

namespace Assets.Scripts.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/Dictionary/Effect")]
    public class EffectDictionary : SingletonDictionary<EffectController>
    {
    }
}