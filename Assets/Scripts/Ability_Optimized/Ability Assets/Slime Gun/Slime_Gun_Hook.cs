using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;
using static UnityEngine.UI.GridLayoutGroup;
using System;


public class Slime_Gun_Hook: MonoBehaviour
{

    private Transform m_parent;
    private Rigidbody m_rb;
    private LineRenderer m_lineRenderer;

    public Action on_hook_planted;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.material.renderQueue = 3100;
        m_lineRenderer.positionCount = 2;
    }

    public void Initialize(Transform parent_transform)
    {
        m_parent = parent_transform;
    }

    public void FireHook(Vector3 dir, float shot_force, Action on_plant)
    {
        Vector3 norm_aim_dir = dir.normalized * 3;
        norm_aim_dir.y = 0.5f;

        Vector3 hook_spawn_pos = m_parent.transform.position + norm_aim_dir;
        hook_spawn_pos.y += m_parent.GetComponent<Collider>().bounds.center.y;

        transform.position = hook_spawn_pos;
        gameObject.SetActive(true);

        m_rb.AddForce(dir * shot_force, ForceMode.Impulse);
    }

    public void RetractHook()
    {
        m_rb.isKinematic = false;
    }

    private void Update()
    {
        m_lineRenderer.SetPosition(0, m_parent.position);
        m_lineRenderer.SetPosition(1, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_rb.isKinematic = true;
        on_hook_planted?.Invoke();
    }



}
