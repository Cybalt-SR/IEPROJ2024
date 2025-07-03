using gab_roadcasting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;

namespace AbilityOP
{

    /// <summary>
    /// Children of Ability must create a class named "{Ability_Name}_data" that inherits from AbilityData
    /// AbilityData is automatically set in the AbilityFactory
    /// </summary>
    public abstract class Ability: Ownable
    {
        protected Dictionary<string, Action<Dictionary<string, object>>> m_passive_handler = new();
        protected object m_ability_data;
        protected CoroutineWrapper m_ability_coroutine;

        public virtual void Register()
        {
            Passive();
            foreach (var passive in m_passive_handler)
                EventBroadcasting.AddListener(passive.Key, passive.Value);
        }
        public virtual void Unregister()
        {
            RemoveOwner();
            foreach (var passive in m_passive_handler)
                EventBroadcasting.RemoveListener(passive.Key, passive.Value);
        }


        /// <summary>
        /// Load passive events in the m_passive_handler attribute here
        /// </summary>
        public virtual void Passive() { }

        protected virtual IEnumerator Active()
        {
            yield break;
        }

        public virtual IEnumerator Cast()
        {
            m_ability_coroutine = new CoroutineWrapper(Active());
            yield return AbilityCoroutine.Run();
        }


        public object AbilityData
        {
            get => m_ability_data;
            set
            {
                string value_type = value.GetType().Name;
                string ability_type = this.GetType().Name;
                string ability_data_type = ability_type + "_data";

                if (!value.GetType().IsSubclassOf(typeof(AbilityData)))
                {
                    Debug.LogError($"[ERROR] {value_type} is not a Data Holder.");
                    return;
                }

                if (value_type == ability_data_type)
                    m_ability_data = value;
                else Debug.LogError($"[ERROR] Data Holder {value_type} is not compatible with {ability_type}.");
            }
        }

        public CoroutineWrapper AbilityCoroutine { 
            get => m_ability_coroutine;
        }

    }

}
