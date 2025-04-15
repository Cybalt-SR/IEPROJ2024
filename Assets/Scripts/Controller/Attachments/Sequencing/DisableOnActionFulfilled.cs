using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnActionFulfilled : MonoBehaviour
{
    [SerializeField] private List<string> Action_defines = new();
    [SerializeField] private int required_count = 0;
    [SerializeField] private int current_count = 0;
    [SerializeField] private List<GameObject> toDisable = new List<GameObject>();

    private void Start()
    {
        ActionWaiter.RegisterNonStrictWaiter(Action_defines.ToArray(), (Transform t) =>
        {
            current_count++;

            if (current_count == required_count)
            {
                foreach (var obj in toDisable)
                {
                    obj.SetActive(false);
                }

                ActionWaiter.Broadcast("opened_locked_door", transform);

                return true;
            }
            else
                return false;
        });
    }
}
