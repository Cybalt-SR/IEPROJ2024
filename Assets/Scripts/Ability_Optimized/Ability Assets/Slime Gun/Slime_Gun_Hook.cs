using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;


public class Slime_Gun_Hook: MonoBehaviour
{

    private Transform m_parent;

    private Rigidbody m_rb;
    private Vector3 m_aim_dir;

    private bool m_hook_locked;
    private float m_shot_force;

    private Vector3? m_hook_pos = null;

    private void Awake()
    {
       m_rb = GetComponent<Rigidbody>();
    }

    public void FireHook(Transform parent, Vector3 dir, float shot_force)
    {
        m_parent = parent;
        m_aim_dir = dir;
        m_shot_force = shot_force;
        gameObject.SetActive(true);
    }
   
    private void FixedUpdate()
    {
        Vector3 shot_dir = (m_parent.position - m_aim_dir).normalized;
        m_rb.AddForce(shot_dir * m_shot_force, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
       //if()
    }



}
