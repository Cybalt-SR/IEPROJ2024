using Assets.Scripts.Library;
using Assets.Scripts.Library.ActionBroadcaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ActionBroadcaster : MonoSingleton<ActionBroadcaster>
{
    public readonly List<(string name, Func<Transform, bool> callback)> waitlist = new();

    public static void Broadcast(string action, Transform where, ref bool received)
    {
        bool removed_atleast_one = false;
        Instance.waitlist.RemoveAll(tuple =>
        {
            if (tuple.name != action)
                return false;

            if (tuple.callback.Invoke(where))
            {
                removed_atleast_one = true;
                return true;
            }

            return false;
        });

        if (removed_atleast_one)
            received = true;
    }
    public static void RegisterWaiter(string action_name, Func<Transform, bool> waiter) => Instance.waitlist.Add((action_name, waiter));
    public static bool CheckIfWaiting(string action_name) => Instance.waitlist.Any(tuple => tuple.name == action_name);
}
