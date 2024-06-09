using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    abstract public class PooledBehaviour<T> : MonoBehaviour where T : PooledBehaviour<T>
    {
        public Queue<T> Pool { get; private set; }

        public void Assign(Queue<T> new_pool)
        {
            Pool = new_pool;
        }

        protected virtual void OnDisable()
        {
            Pool.Enqueue(this as T);
        }
    }
}
