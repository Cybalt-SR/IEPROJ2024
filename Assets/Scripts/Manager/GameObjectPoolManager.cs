using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GameObjectPoolManager : Manager_Base<GameObjectPoolManager>
    {
        [Header("References")]
        [SerializeField] protected List<GameObject> pool;
        [SerializeField] protected GameObject poolable;

        [Header("Config")]
        [SerializeField] protected int poolSize = 10;
        [SerializeField] protected bool expandable = true;

        public int totalPoolSize { get { return pool.Count; } }
        public int available { get { int i = 0; pool.ForEach(p => { if (p.activeInHierarchy) ++i; }); return i; } }

        protected override void Awake()
        {
            base.Awake();
            pool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
                create();
        }

        public GameObject RequestOrCreate()
        {

            bool isPoolEmpty()
            {
                foreach (var i in pool)
                    if (i.activeInHierarchy)
                        return false;
                return true;
            }

            GameObject get = null;

            if (isPoolEmpty())
                return expandable ? create() : null;

            foreach (var i in pool)
            {
                if (!i.activeInHierarchy)
                {
                    get = i;
                    i.SetActive(true);
                    break;
                }
            }

            return get;
        }

        public void Release(GameObject toRelease)
        {
            if (pool.Contains(toRelease))
                toRelease.SetActive(false);
        }

        public virtual void ReleaseAll()
        {
            foreach(var i in pool)
                Release(i);
        }

        protected GameObject create()
        {
            GameObject toCreate = Instantiate(poolable);
            toCreate.name = poolable.name;
            toCreate.transform.parent = transform;
            toCreate.SetActive(true);
            pool.Add(toCreate);

            return toCreate;
        }

    }
}