using gab_roadcasting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;

namespace AbilityOP
{
    public abstract class Ability 
    {
        protected Dictionary<string, Action<Dictionary<string, object>>> m_passive_handler = new();
        public GameObject m_owner;

        protected object m_ability_data;
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

        public Ability()
        {
            Passive();
        }

        public virtual void Register()
        {
            foreach (var passive in m_passive_handler)
                EventBroadcasting.AddListener(passive.Key, passive.Value);
        }
        public virtual void Unregister()
        {
            foreach (var passive in m_passive_handler)
                EventBroadcasting.RemoveListener(passive.Key, passive.Value);
        }

        /// <summary>
        /// Load passive events in the m_passive_handler attribute here
        /// </summary>
        public virtual void Passive() { }
        public virtual void Update(float deltaTime) { }
        public abstract Task Cast();

    }

}
