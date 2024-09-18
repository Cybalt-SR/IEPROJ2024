using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ActionBroadcaster : MonoSingleton<ActionBroadcaster>
{
    public readonly UnityEvent<string, Transform> onAction = new();
    public readonly List<(string name, Func<bool> callback)> waitlist = new List<(string, Func<bool>)>();

    public static void Broadcast(string action, Transform where, ref bool received)
    {
        Instance.waitlist.RemoveAll(tuple => tuple.name == action);
        Instance.onAction.Invoke(action, where);
    }
    public static void Subscribe(Action<string, Transform> action) => Instance.onAction.AddListener(action.Invoke);
    public static void WaitFor(string action_name, Func<bool> callback) => Instance.waitlist.Add((action_name, callback));
    public static bool CheckIfWaiting(string action_name) => Instance.waitlist.Any(tuple => tuple.name == action_name);
}
