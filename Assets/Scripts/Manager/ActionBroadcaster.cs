using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionBroadcaster : MonoSingleton<ActionBroadcaster>
{
    public readonly UnityEvent<string, Transform> onAction = new();
    public readonly UnityEvent<string> onNewWait = new();
    public static void Broadcast(string action, Transform where) => Instance.onAction.Invoke(action, where);
    public static void Subscribe(Action<string, Transform> action) => Instance.onAction.AddListener(action.Invoke);
    public static void WaitFor(string action_name) => Instance.onNewWait.Invoke(action_name);
    public static void AssignWaiter(Action<string> action) => Instance.onNewWait.AddListener(action.Invoke);
}
