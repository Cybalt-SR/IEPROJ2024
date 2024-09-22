using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ActionBroadcaster : MonoSingleton<ActionBroadcaster>
{
    public readonly List<(string name, Func<Transform, bool> callback)> waitlist = new List<(string, Func<Transform, bool>)>();

    public static void Broadcast(string action, Transform where, ref bool received)
    {
        Instance.waitlist.RemoveAll(tuple =>
        {
            if (tuple.name != action)
                return false;

            if (tuple.callback.Invoke(where))
                return true;

            return false;
        });
    }
    public static void WaitFor(string action_name, Func<Transform, bool> callback) => Instance.waitlist.Add((action_name, callback));
    public static bool CheckIfWaiting(string action_name) => Instance.waitlist.Any(tuple => tuple.name == action_name);
}
