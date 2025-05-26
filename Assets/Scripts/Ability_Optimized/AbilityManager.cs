using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace AbilityOP
{

    /*
        [TO-DO]
        - Ownership Handling
        - Duplicate Handling
        - Secondary Handling
        - Recreate Weapons
    */

    public class AbilityManager : Manager_Base<AbilityManager>
    {
        private AbilityFactory m_ability_factory = new();
        private Dictionary<GameObject, List<AbilityHandler>> m_ability_updatables = new();

        private void Update()
        {
            foreach (List<AbilityHandler> abilities in m_ability_updatables.Values)
            {
                foreach (AbilityHandler ability in abilities)
                    ability.Update(Time.deltaTime);
            }

        }

        public bool RequestAbility(GameObject owner, string AbilityName, bool setActiveByDefault = true)
        {
            if (!m_ability_updatables.ContainsKey(owner))
                m_ability_updatables[owner] = new();

            Ability ability = m_ability_factory.RequestAbility(AbilityName);

            if (ability != null)
            {
                AbilityHandler handler = new(ability, setActiveByDefault);
                handler.m_ability.m_owner = owner;
                m_ability_updatables[owner].Add(handler);
                return true;
            }

            return false;
        }

        public bool ReleaseAbilities(GameObject owner)
        {
            if (!m_ability_updatables.ContainsKey(owner))
                return false;

            List<AbilityHandler> handlers = m_ability_updatables[owner];

            while (handlers.Count > 0)
            {
                m_ability_factory.UnloadAbility(handlers[0].m_ability);
                handlers.RemoveAt(0);
            }

            return true;
        }

    }

    internal class AbilityHandler
    {
        //References
        internal Ability m_ability;

        //States
        internal bool m_is_running { get => m_execution != null && !m_execution.IsCompleted; }
        internal bool m_is_active;
        internal Task m_execution;

    

        //Data
        internal float m_current_cooldown = 0;

        internal AbilityHandler(Ability ability, bool setActiveByDefault = true)
        {
            m_ability = ability;
            m_is_active = setActiveByDefault;
            m_execution = null;
        }

        internal void Update(float deltaTime)
        {
            if (!m_is_active)
                return;

            if (m_current_cooldown > 0)
                m_current_cooldown = Mathf.Max(0, m_current_cooldown - deltaTime);
        }

        internal async Task Activate()
        {
            if (!m_is_active || m_is_running || m_current_cooldown > 0 )
                return;

            var data = m_ability.AbilityData as AbilityData;

            if(data.StartCooldownOnCast)
                m_current_cooldown = data.Cooldown;

            m_execution = m_ability.Cast();
            await m_execution;

            if (!data.StartCooldownOnCast)
                m_current_cooldown = data.Cooldown;
        }

    }

   

}
