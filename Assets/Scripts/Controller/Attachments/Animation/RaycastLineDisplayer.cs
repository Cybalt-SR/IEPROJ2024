using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastLineDisplayer : MonoBehaviour
{
    [SerializeField] private float max_length;

    private LineRenderer mLineRenderer;

    private void Awake()
    {
        mLineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        Vector3 endpos;

        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit info, max_length))
            endpos = Vector3.forward * info.distance;
        else
            endpos = Vector3.forward * max_length;

        mLineRenderer.SetPosition(1, endpos);
    }
}
