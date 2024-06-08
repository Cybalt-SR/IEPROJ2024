using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Library
{
    public abstract class SingletonResource<T> : ScriptableObject where T : ScriptableObject
    {
        public static T Instance
        {
            get
            {
                foreach (var item in Resources.LoadAll("", typeof(T)).Cast<T>())
                {
                    return item;
                }

                return null;
            }
        }
    }
}