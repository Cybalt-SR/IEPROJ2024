using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSeeBroadcaster : MonoBehaviour
{
    [SerializeField] private string see_;
    [SerializeField] private bool only_when_waiting = true;
    [SerializeField] private bool only_once = false;

    private bool waiting = false;

    private void Start()
    {
        ActionBroadcaster.AssignWaiter(action_name =>
        {
            if (action_name == "see_" + see_)
                waiting = true;
        });
    }

    private void Update()
    {
        if (only_when_waiting && !waiting)
            return;

        if (CameraController.Instance.PointInCameraView(this.transform.position))
        {
            ActionBroadcaster.Broadcast("see_" + see_, this.transform);
            waiting = false;

            if (only_once)
                this.enabled = false;
        }
    }
}
