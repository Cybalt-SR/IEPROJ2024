using gab_roadcasting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInvoker : MonoBehaviour
{
    [SerializeField] private string EventName;


    public void OnInvoke()
    {
        EventBroadcasting.InvokeEvent(EventName, null);
    }
}
