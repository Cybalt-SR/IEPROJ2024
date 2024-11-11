using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(SphereCollider))]
public class ProximityChecker : MonoBehaviour
{
    [SerializeField] protected List<GameObject> collisionList;
    [SerializeField] protected List<string> tagWhitelist;

    public float MaxDistance { get => GetComponent<SphereCollider>().radius; }

    public List<GameObject> CollisionList { get => collisionList; }


    public Action<GameObject> OnProximityEntered;
    public Action<GameObject> OnProximityExit;
    public Action OnClear;

    private void OnTriggerEnter(Collider other)
    {

        if (tagWhitelist.Contains(other.tag))
        {
            collisionList.Add(other.gameObject);
            OnProximityEntered?.Invoke(other.gameObject);
        }
               
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagWhitelist.Contains(other.tag) == false)
            return;

        if (collisionList.Contains(other.gameObject))
        {
            collisionList.Remove(other.gameObject);
            OnProximityExit?.Invoke(other.gameObject);
        }
           

    }

    public void Clear()
    {
        collisionList.RemoveAll(obj => obj.activeInHierarchy);
        OnClear?.Invoke();
    }

}
