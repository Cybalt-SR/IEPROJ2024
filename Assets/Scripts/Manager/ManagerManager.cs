using Assets.Scripts.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Manager
{
    public class ManagerManager : MonoBehaviour, ISingleton<ManagerManager>
    {
        private void Awake()
        {
            Type parentType = typeof(Manager_Base<>);
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes();

            types = types.Where(t => t.BaseType != null && t.BaseType.IsConstructedGenericType);
            types = types.Where(t => t.BaseType.GetGenericTypeDefinition() == parentType);

            var selectedTypes = types.ToArray();
            var requiredTypes = new Type[3];

            foreach (Type manager_type in selectedTypes)
            {
                requiredTypes[0] = manager_type.GetAttributeValue((RequireComponent requirements) => requirements.m_Type0);
                requiredTypes[1] = manager_type.GetAttributeValue((RequireComponent requirements) => requirements.m_Type1);
                requiredTypes[2] = manager_type.GetAttributeValue((RequireComponent requirements) => requirements.m_Type2);

                var new_gameObject = new GameObject(manager_type.Name);
                new_gameObject.transform.parent = transform;

                foreach (var requiredType in requiredTypes)
                {
                    if (requiredType == null)
                        continue;

                    new_gameObject.AddComponent(requiredType);
                }

                new_gameObject.AddComponent(manager_type);
            }
        }
    }
}