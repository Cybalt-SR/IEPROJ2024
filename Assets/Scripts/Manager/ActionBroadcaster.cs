using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionBroadcaster : MonoSingleton<ActionBroadcaster>
{
    public readonly UnityEvent<string, Transform> onAction = new();
    public readonly List<string> waitlist = new List<string>();

    public static void Broadcast(string action, Transform where)
    {
        Instance.waitlist.RemoveAll(name => name == action);

        Instance.onAction.Invoke(action, where);
    }
    public static void Subscribe(Action<string, Transform> action) => Instance.onAction.AddListener(action.Invoke);
    public static void WaitFor(string action_name) => Instance.waitlist.Add(action_name);
    public static bool CheckIfWaiting(string action_name) => Instance.waitlist.Contains(action_name);
}
