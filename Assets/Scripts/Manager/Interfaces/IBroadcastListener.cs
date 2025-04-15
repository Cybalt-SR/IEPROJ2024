using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBroadcastListener<T> where T : MonoBehaviour
{
    public Dictionary<string, Action<T>> action_set { get; }
}
