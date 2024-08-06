using Assets.Scripts.Controller;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string x_name = "x_speed";
    [SerializeField] string y_name = "y_speed";
    [SerializeField] string z_name = "z_speed";
    [SerializeField] float zero_threshold = 0.1f;
    [SerializeField] bool format = false;

    public void OnVector3Event(Vector3 direction)
    {
        direction = Quaternion.AngleAxis(-CameraController.Instance.Camera.transform.eulerAngles.y, Vector3.up) * direction;

        void ImposeThreshold(ref float value) { if (Mathf.Abs(value) < zero_threshold) value = 0; }

        ImposeThreshold(ref direction.x);
        ImposeThreshold(ref direction.y);
        ImposeThreshold(ref direction.z);

        if (format)
        {
            int axesabovethreshold = 0;
            void CountAboveThreshold(float value) { if (Mathf.Abs(value) < 0.7f) axesabovethreshold++; }
            void FlattenMinorAxes(ref float value) { if (Mathf.Abs(value) < 1.0f) value = 0; }
            void ImposeMinorAxes(ref float value)
            {
                if (Mathf.Abs(value) > 0.5f)
                    value = 0.7f * Mathf.Sign(value);
                else
                    value = 0;
            }

            float highest = Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.y), Mathf.Abs(direction.z));
            direction /= highest;

            CountAboveThreshold(direction.x);
            CountAboveThreshold(direction.y);
            CountAboveThreshold(direction.z);

            if (axesabovethreshold == 1)
            {
                FlattenMinorAxes(ref direction.x);
                FlattenMinorAxes(ref direction.y);
                FlattenMinorAxes(ref direction.z);
            }
            else
            {
                ImposeMinorAxes(ref direction.x);
                ImposeMinorAxes(ref direction.y);
                ImposeMinorAxes(ref direction.z);
            }
        }

        if (x_name.Length > 0)
            animator.SetFloat(x_name, direction.x);
        if (y_name.Length > 0)
            animator.SetFloat(y_name, direction.y);
        if (z_name.Length > 0)
            animator.SetFloat(z_name, direction.z);
    }
}
