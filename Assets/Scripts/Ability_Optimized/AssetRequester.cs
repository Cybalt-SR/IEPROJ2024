using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityOP
{
    public class AssetRequester : Manager_Base<AssetRequester>
    {

        private Dictionary<string, List<GameObject>> m_storage = new();

        public static GameObject WithdrawAsset(string name, Transform new_parent = null)
        {
            if (Instance.m_storage.ContainsKey(name) && Instance.m_storage[name].Count > 0)
            {
                GameObject asset = Instance.m_storage[name][0];
                Instance.m_storage[name].RemoveAt(0);


                if (new_parent)
                    asset.transform.parent = new_parent;

                return asset;
            }
          
            return null;  
        }

        public static bool LoadAsset(string asset_name, out GameObject to_load, Transform new_parent = null) 
        {
            Object o = Resources.Load($"Ability Asset/{asset_name}");
            to_load =  o as GameObject ?? new GameObject(asset_name);
            if(new_parent)
                to_load.transform.parent = new_parent;
            return o != null;
        }

        public static bool LoadAsset<T>(string asset_name, out T to_load, Transform new_parent = null) where T : Component
        {
            Object o = Resources.Load($"Ability Asset/{asset_name}");
            GameObject go = o as GameObject ?? new GameObject(asset_name);
            to_load = go.GetComponent<T>() ?? go.AddComponent<T>();
            if (new_parent)
               go.transform.parent = new_parent;
            return o != null;
        }

        public static void DepositAsset(string name, GameObject to_deposit)
        {
            List<GameObject> object_storage;
            if (Instance.m_storage.ContainsKey(name))
            {
               object_storage = Instance.m_storage[name];
            }
            else
            {
                object_storage = new List<GameObject>();
                Instance.m_storage.Add(name, object_storage);
            }

            object_storage.Add(to_deposit);
            to_deposit.name = name;
            to_deposit.transform.SetParent(Instance.transform, true);
            to_deposit.SetActive(false);
        }
    }
}