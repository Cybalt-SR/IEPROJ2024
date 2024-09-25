using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class ProximityChecker : MonoBehaviour
{
    [SerializeField] private List<GameObject> collisionList;
    [SerializeField] private List<string> tagWhitelist;

    public List<GameObject> CollisionList { get { return collisionList; } }

    private void OnTriggerEnter(Collider other)
    {
        if(tagWhitelist.Contains(other.tag))
            collisionList.Add(other.gameObject);    
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagWhitelist.Contains(other.tag) && collisionList.Contains(other.gameObject))
            collisionList.Remove(other.gameObject);
    }

}
