using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectPoolManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected List<GameObject> pool;
    [SerializeField] protected GameObject poolable;
    [SerializeField] private Transform proxy_parent;

    [Header("Config")]
    [SerializeField] protected int poolSize = 10;
    [SerializeField] protected bool expandable = true;

    public int totalPoolSize { get { return pool.Count; } }
    public int available { get { int i = 0; pool.ForEach(p => { if (p.activeInHierarchy) ++i; }); return i; } }

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
        foreach (var i in pool)
            Release(i);
    }

    protected virtual void attachComponents(GameObject go)
    {

    }

    protected GameObject create()
    {
        GameObject toCreate = Instantiate(poolable);
        toCreate.transform.SetParent(proxy_parent, false);
        toCreate.name = poolable.name;

        toCreate.SetActive(true);
        attachComponents(toCreate);
        pool.Add(toCreate);

        return toCreate;
    }
    private void Awake()
    {

        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
            create();
    }



}

