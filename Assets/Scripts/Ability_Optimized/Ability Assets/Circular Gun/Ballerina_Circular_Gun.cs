using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballerina_Circular_Gun : MonoBehaviour
{

    private List<EnemyController> trapped_enemies = new();
    [SerializeField] private Transform center;

    public float lifetime = 8f;

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            center.gameObject.SetActive(false); 
    }


    private void OnTriggerEnter(Collider other)
    {
        var controller = other.GetComponent<EnemyController>();
        if (controller)
        {
            trapped_enemies.Add(controller);
            controller.enabled = false;
            other.transform.LookAt(center.position);
        }
    }

    private void OnDestroy()
    {
        foreach(var e in trapped_enemies)
        {
            if(e)
                e.enabled = true;
        }
    }

}
