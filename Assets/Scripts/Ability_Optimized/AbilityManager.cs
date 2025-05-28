using Assets.Scripts.Gameplay.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace AbilityOP
{

    /*
        [TO-DO]
        - Do Bugfixing
        - Recreate Weapons
    */

    public class AbilityManager : Manager_Base<AbilityManager>
    {
        private AbilityFactory m_ability_factory = new();

        private Dictionary<GameObject, List<AbilityHandler>> m_abilities = new();
        private List<AbilityHandler> m_updatables = new();

        public async Task InvokeAbility(GameObject owner, string ability_name)
        {
            if (!m_abilities.ContainsKey(owner))
            {
                Debug.LogError($"{owner.name} has no abilities assigned.");
                return;
            }
                
            foreach (AbilityHandler handler in m_abilities[owner])
            {
                if (handler.m_ability.GetType().Name == ability_name)
                {
                    await handler.Activate();
                    return;
                }
            }

            Debug.LogError($"{owner.name} has no such abilities named {ability_name}.");
        }

        private void Update()
        {
            foreach(AbilityHandler handler in m_updatables)
                handler.Update(Time.deltaTime);
        }

        public bool RequestAbility(GameObject owner, string AbilityName, bool setActiveByDefault = true)
        {
            if (!m_abilities.ContainsKey(owner))
                m_abilities[owner] = new();

            Ability ability = m_ability_factory.RequestAbility(AbilityName);

            if (ability != null)
            {
                if (ContainsAbility(m_abilities[owner], ability.GetType()))
                {
                    Debug.LogError($"[ERROR] {owner.name} already owns {AbilityName}");
                    return false;
                }
                    

                AbilityHandler handler = new(ability, owner, setActiveByDefault);
                m_abilities[owner].Add(handler);
                m_updatables.Add(handler);
                return true;
            }

            return false;
        }

        public bool ReleaseAbilities(GameObject owner)
        {
            if (!m_abilities.ContainsKey(owner))
                return false;

            List<AbilityHandler> handlers = m_abilities[owner];

            while (handlers.Count > 0)
            {
                m_updatables.Remove(handlers[0]);
                m_ability_factory.UnloadAbility(handlers[0].m_ability);
                handlers.RemoveAt(0);
            }

            Debug.LogWarning($"[DEBUG] Released all abilities for {owner.name} successfully.");
            return true;
        }

        private bool ContainsAbility(List<AbilityHandler> handlers, Type Ability)
        {
            foreach(AbilityHandler handler in handlers)
            {
                if (handler.m_ability.GetType() == Ability)
                    return true;
            }
            return false;
        }


        public float GetRemainingCooldown(GameObject owner, string ability_name, bool get_percentage = false)
        {
            if (!m_abilities.ContainsKey(owner))
            {
                Debug.LogError($"[ERROR] {owner.name} does not have abilities.");
                return 0f;
            }
               

            List<AbilityHandler> handler = m_abilities[owner];

            foreach(AbilityHandler ability in handler)
            {
                if(ability.m_ability.GetType().Name == ability_name)
                {
                    return !get_percentage ? ability.m_current_cooldown: 
                        ability.m_current_cooldown / (ability.m_ability.AbilityData as AbilityData).Cooldown;
                }
            }


            Debug.LogError($"[ERROR] {owner.name} does not own a \"{ability_name}\"");
            return 0f;
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

        internal AbilityHandler(Ability ability, GameObject owner, bool setActiveByDefault = true)
        {
            m_ability = ability;
            m_ability.m_owner = owner;
            m_is_active = setActiveByDefault;
            m_execution = null;
        }

        internal void Update(float deltaTime)
        {
            if (!m_is_active)
                return;

            if (m_current_cooldown > 0)
                m_current_cooldown = Mathf.Max(0, m_current_cooldown - deltaTime);

            m_ability.Update(deltaTime);
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
