using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class DetectionSphere : MonoBehaviour
{
    private List<GameObject> inside = new();
    public List<string> whitelist = new();

    private void OnTriggerEnter(Collider other)
    {
        if (whitelist.Contains(other.tag))
            inside.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(inside.Contains(other.gameObject))
            inside.Remove(other.gameObject);
    }

    public List<GameObject> Within { get => new(inside); }
}
