using Assets.Scripts.Controller;
using Assets.Scripts.Data.AssetDictionaries;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class PoolManager<TManager, TObject> : Manager_Base<TManager> where TObject : PooledBehaviour<TObject> where TManager : MonoBehaviour
    {
        private readonly Dictionary<string, Queue<TObject>> Pool = new();

        public TObject RequestOrCreate(string id)
        {
            TObject Create()
            {
                var newobject = Instantiate(SingletonResource<SingletonDictionary<TObject>>.Instance.Get(id).gameObject, ISingleton<PoolParent>.Instance.transform);
                var component = newobject.GetComponent<TObject>();

                if (Pool.ContainsKey(id) == false)
                    Pool.Add(id, new());

                component.Assign(Pool[id]);
                return component;
            }

            if (Pool.ContainsKey(id) == false)
                return Create();

            if (Pool[id].TryDequeue(out TObject pooledbehaviour))
            {
                pooledbehaviour.gameObject.SetActive(true);
                return pooledbehaviour;
            }

            return Create();
        }
    }
}
