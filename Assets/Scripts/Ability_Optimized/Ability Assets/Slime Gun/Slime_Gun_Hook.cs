using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;


public class Slime_Gun_Hook : Hitbox
{

    public GameObject parent;

    private void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override List<GameObject> CaptureCollisions()
    {
        return null;
    }

}
