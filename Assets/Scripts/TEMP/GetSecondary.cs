using gab_roadcasting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GetSecondary : MonoBehaviour
{
    [SerializeField] private Secondary secondary;

    private Dictionary<string, object> param = new();

    private void Awake()
    {
        param.Add("Secondary", secondary);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        EventBroadcasting.InvokeEvent(EventNames.SECONDARY_EVENTS.ON_SECONDARY_EQUIP, param);
        Destroy(gameObject);
    }
  
}
