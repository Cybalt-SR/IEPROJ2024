using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.UI.GridLayoutGroup;

namespace AbilityOP
{

    public class AbilityFactory
    {

        public static string ABILITY_DATA_PATH = "Ability Data/";

        private List<Type> m_ability_types;
        private List<string> m_ability_names;

        private Dictionary<string, List<Ability>> m_ability_pool = new();


        public AbilityFactory() {
            GetTypes();
        }

        /// <summary>
        /// Retrieves all the children of ability and stores them in a list
        /// </summary>
        public void GetTypes()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> types = assembly.GetTypes().ToList();
            m_ability_types = types.Where(t => t.IsSubclassOf(typeof(Ability)) && !t.IsAbstract).ToList();
            m_ability_names = types.Select(t => t.Name).ToList();
        }

        /// <summary>
        /// Requests an ability from the ability pool or creates one if there's no inactive instance remaining
        /// </summary>
        public Ability RequestAbility(string ability_name)
        {
            if (!m_ability_names.Contains(ability_name)) 
            {
                Debug.LogError($"[ERROR] No such Ability with the name {ability_name}.");
                return null;
            }

            if(m_ability_pool.ContainsKey(ability_name) && m_ability_pool[ability_name].Count > 0)
            {
                Ability poolable = m_ability_pool[ability_name][0];
                m_ability_pool[ability_name].RemoveAt(0);
                poolable.Register();
                Debug.LogWarning($"[DEBUG] Retrieved an instance of {ability_name} from the pool");
                return poolable;
            }


            if (!m_ability_pool.ContainsKey(ability_name))
                m_ability_pool[ability_name] = new();

            AbilityData data = Resources.Load("Ability Data/" + ability_name, typeof(AbilityData)) as AbilityData;

            if (data == null)
            {
                Debug.LogError($"[ERROR] No \"{ability_name}\" found. Please create one in Resources/Ability Data.");
                return null;
            }
                
           
            var new_poolable = Activator.CreateInstance(m_ability_types.Find(t => t.Name == ability_name)) as Ability;
            new_poolable.AbilityData = data;


            if (Validate(new_poolable))
            {
                new_poolable.Register();
                Debug.LogWarning($"[DEBUG] Created an instance of {ability_name}");
                return new_poolable;
            }

            return null;  
        }

        /// <summary>
        /// Returns param ability to the Ability Pool
        /// </summary>
        public void UnloadAbility(Ability ability)
        {
            ability.Unregister();
            ability.m_owner = null;
            m_ability_pool[ability.GetType().Name].Add(ability);
        }

        /// <summary>
        /// Checks if a data holder scriptable object exists for ability and if one has been created in Ability Data in the Resources Folder
        /// </summary>
        /// <returns>Returns true if a data holder exists and an instance of it is found in the Ability Data folder</returns>
        public bool Validate(Ability ability)
        {
            string type_name = ability.GetType().Name;
            string data_holder_name = type_name + "_data";
            Type data_holder_type = Type.GetType(data_holder_name);

            if (data_holder_type == null)
            {
                Debug.LogError($"[ERROR] No Data Holder exists for {type_name}. Please create one named {type_name}_data");
                return false;
            }

            var data_holder = Resources.Load(AbilityFactory.ABILITY_DATA_PATH + type_name);

            if (data_holder == null)
            {
                Debug.LogError($"[ERROR] No Ability Data scriptable object exists for {type_name}. Please create one named {type_name}");
                return false;
            }

            return true;
        }

    }

}
