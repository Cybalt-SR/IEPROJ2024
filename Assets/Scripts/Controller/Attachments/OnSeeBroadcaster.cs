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
        if (only_when_waiting && ActionBroadcaster.CheckIfWaiting("see_" + see_) == false)
            return;

        if (CameraController.Instance.PointInCameraView(this.transform.position))
        {
            ActionBroadcaster.Broadcast("see_" + see_, this.transform);

            if (only_once)
                this.enabled = false;
        }
    }
}
