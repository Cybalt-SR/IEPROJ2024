using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Manager
{
    public class Manager_Base<T> : MonoBehaviour, ISingleton<T> where T : MonoBehaviour
    {
        protected private static T Instance { get => ISingleton<T>.Instance; private set => ISingleton<T>.Instance = value; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this as T;
        }
    }
}