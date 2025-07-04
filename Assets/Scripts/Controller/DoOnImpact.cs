using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoOnImpact : MonoBehaviour
{
    public Action impact_event;
    public bool exclude_floor;
    public List<string> tag_filter = new();

    public void OnCollisionEnter(Collision collision)
    {
       
        if(exclude_floor && Vector3.Dot(collision.contacts[0].normal, Vector3.down) >  0.8f || tag_filter.Contains(collision.gameObject.tag))
        {
           Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
            print($"ignored {collision.gameObject.name}, { Vector3.Dot(collision.contacts[0].normal, Vector3.up)}");
        }
        else
        {
            print(collision.gameObject.name);
            impact_event?.Invoke();
        }
    }


}
