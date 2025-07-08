using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class DetectionSphere : MonoBehaviour
{
    protected List<GameObject> inside = new();
    public List<string> whitelist = new();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (whitelist.Contains(other.tag))
            inside.Add(other.gameObject);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(inside.Contains(other.gameObject))
            inside.Remove(other.gameObject);
    }

    public List<GameObject> Within { get => new(inside); }
}
