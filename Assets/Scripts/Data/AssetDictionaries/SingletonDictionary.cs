using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Data.AssetDictionaries
{
    public class SingletonDictionary<T, F> : SingletonResource<F> where F : ScriptableObject where T : class
    {
        [SerializeField] private T fallback;
        [SerializeField] private SerializableDictionary<string, T> dictionary = new("");

        public T Get(string id)
        {
            if (dictionary.ContainsKey(id))
                return dictionary[id];

            return fallback;
        }
    }

    public class SingletonDictionary<T> : SingletonResource<SingletonDictionary<T>> where T : class
    {
        [SerializeField] private T fallback;
        [SerializeField] private SerializableDictionary<string, T> dictionary = new("");

        public T Get(string id)
        {
            if (dictionary.ContainsKey(id))
                return dictionary[id];

            return fallback;
        }
    }
}