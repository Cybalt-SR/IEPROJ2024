using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Gun_Grappler : MonoBehaviour
{

    public Action on_collision;
    public List<string> whitelist = new();

    private bool m_is_grappling = false;
    private List<Collider> ignore_list = new();

    private Vector3 m_hook_pos;
    private float m_pull_force;

    private Rigidbody m_rb;
    private Collider m_coll;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_coll = GetComponent<Collider>();
    }

    public void StartGrappling(Vector3 hook_pos, float pull_force)
    {
        m_hook_pos = hook_pos;
        m_is_grappling = true;
        GetComponent<UnitController>().enabled = false;
        m_pull_force = pull_force;
    }

    public void EndGrappling()
    {
        m_is_grappling = false;

        foreach (var c in ignore_list)
            Physics.IgnoreCollision(c, m_coll, false);

        ignore_list.Clear();
        m_rb.velocity = Vector3.zero;
        GetComponent<UnitController>().enabled = true;
        on_collision?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_is_grappling && Vector3.Dot(collision.contacts[0].normal, Vector3.up) < 0.8f)
        {
            if (whitelist.Contains(collision.gameObject.tag))
                Physics.IgnoreCollision(collision.collider, m_coll);
            else EndGrappling();
        }
    }

    private void FixedUpdate()
    {
        if (m_is_grappling)
        {
            var dir = (m_hook_pos - transform.position).normalized;
            m_rb.AddForce(dir * m_pull_force);

            var t = GetComponentInChildren<DirectionalAnimator3D>();
            if(t) t.transform.LookAt(m_hook_pos);
        }
    }

    public bool is_grappling { get => m_is_grappling;  }

    private void OnDestroy()
    {
        EndGrappling();
    }

}
