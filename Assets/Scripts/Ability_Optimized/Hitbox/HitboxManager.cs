using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityOP
{
    public class HitboxManager : Manager_Base<HitboxManager>
    {
        private Dictionary<string, Hitbox> m_hitboxes = new();
        private Dictionary<string, List<Hitbox>> m_hitbox_pool = new();
        private Dictionary<string, List<Hitbox>> m_hitbox_active = new();

        protected override void Awake()
        {
            base.Awake();

        }

        private void Update()
        {
            
        }
    }
}
