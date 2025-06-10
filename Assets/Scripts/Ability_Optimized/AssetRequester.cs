using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityOP
{
    public class AssetRequester : Manager_Base<AssetRequester>
    {

        private Dictionary<string, List<GameObject>> m_storage = new();

        public T RequestComponent<T>(string name, Transform parent = null) where T : Component
        {
            GameObject go;

            if (m_storage.ContainsKey(name) && m_storage[name].Count > 0)
            {
                go = m_storage[name][0];
                m_storage[name].Remove(go); 
            }
            else go = new GameObject(name); 
            
            if (parent)
                go.transform.SetParent(parent);
            
            return go.GetComponent<T>();
        }

        public GameObject RequestAsset(string name)
        {
            return Resources.Load($"weapon/{name}") as GameObject;
        }

        public void DepositAsset(string name, GameObject to_deposit)
        {
            List<GameObject> object_storage;
            if (m_storage.ContainsKey(name))
            {
               object_storage = m_storage[name];
            }
            else
            {
                object_storage = new List<GameObject>();
                m_storage.Add(name, object_storage);
            }

            object_storage.Add(to_deposit);
            to_deposit.transform.SetParent(transform, true);
        }
    }
}