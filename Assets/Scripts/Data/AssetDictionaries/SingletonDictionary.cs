using Assets.Scripts.Library;
using System.Linq;
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
        public T Get(int index)
        {
            if (index < 0)
                return fallback;

            if(index < dictionary.Count)
                return dictionary.ElementAt(index).Value;

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

        public T Get(int index)
        {
            if (index < 0)
                return fallback;

            if (index < dictionary.Count)
                return dictionary.ElementAt(index).Value;

            return fallback;
        }
    }
}