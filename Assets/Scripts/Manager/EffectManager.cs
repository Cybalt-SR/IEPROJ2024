using Assets.Scripts.Controller;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class EffectManager : PoolManager<EffectManager, EffectController>
    {
        public void SpawnEffect(string effect_id, Vector3 position)
        {
            var newobj = RequestOrCreate(effect_id);
        }
    }
}
