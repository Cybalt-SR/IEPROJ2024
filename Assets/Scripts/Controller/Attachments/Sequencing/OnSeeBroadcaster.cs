using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSeeBroadcaster : MonoBehaviour
{
    [SerializeField] private string see_;
    [SerializeField] private bool only_when_waiting = true;
    [SerializeField] private bool only_once = false;

    private void Update()
    {
        if (only_when_waiting && ActionWaiter.CheckIfWaiting("see_" + see_) == false)
            return;

        if (CameraController.Instance.PointInCameraView(this.transform.position))
        {
            ActionWaiter.Broadcast("see_" + see_, this.transform, out bool received);

            if (received == false)
                return;

            if (only_once)
                this.enabled = false;
        }
    }
}
