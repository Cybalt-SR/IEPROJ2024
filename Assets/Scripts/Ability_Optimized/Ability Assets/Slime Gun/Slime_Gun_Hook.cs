using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;
using System;

public class Slime_Gun_Hook: MonoBehaviour
{
    public Action on_hook_planted;
    public Action on_hook_retracted;
    
    //external
    public Transform firing_reference;
    public Vector3? hook_plant_pos;
    public float lollipop_multiplier;
    public float max_tether_distance;
    public float retract_speed;

    //references
    private Rigidbody m_rb;
    private SphereCollider m_coll;
    private LineRenderer m_lineRenderer;

    //Set on fire vars
    private Vector3 m_dir;
    private float m_speed;
    private float m_max_projectile_distance;

    //Others
    private float m_coll_rad;
    private bool m_lollipop = false;
    private bool is_retracting = false;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_coll = GetComponent<SphereCollider>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.material.renderQueue = 3100;
        m_lineRenderer.positionCount = 2;

        m_coll_rad = m_coll.radius;
    }

    public void FireHook(Vector3 dir, float shot_force, float max_projectile_distance = -1)
    {
        gameObject.SetActive(true);
        transform.position = firing_reference.position;

        m_dir = dir;
        m_speed = shot_force;
        m_max_projectile_distance = max_projectile_distance;

        //reset colldier after lollipop checking
        m_coll.enabled = true;
        m_coll.radius = m_coll_rad;
        m_lollipop = false;
    }

    public void RetractHook()
    {
        //Pull hook back to player
        IEnumerator StartRetracting()
        {
            is_retracting = true;
            m_rb.velocity = Vector3.zero;
            m_coll.enabled = false;
            while(Vector3.Distance(firing_reference.position, transform.position) > 0.5)
            {
                transform.position = Vector3.MoveTowards(transform.position, firing_reference.position, Time.deltaTime * retract_speed);
                yield return null;
            }
            gameObject.SetActive(false);
            is_retracting = false;
            on_hook_retracted?.Invoke();
        }

        hook_plant_pos = null;
        m_rb.isKinematic = false;

        //checker if player unequips midgrapple
        if(gameObject.activeSelf)
            StartCoroutine(StartRetracting());
   
    }

    private void LateUpdate()
    {
        m_lineRenderer.SetPosition(0, firing_reference.position);
        m_lineRenderer.SetPosition(1, transform.position);

        if (hook_plant_pos != null)
        {
            transform.position = hook_plant_pos.Value;
            //Check if player is too far from hook
            if(Vector3.Distance(firing_reference.transform.position, hook_plant_pos.Value) > max_tether_distance)
                RetractHook(); 
            return;
        }
        
        //if after lollipopping and hook isnt planted yet, retract it
        if (m_lollipop)
        {
            RetractHook();
            return;
        }

        //invoke lollipopping
        if (m_max_projectile_distance > 0 && Vector3.Distance(firing_reference.transform.position, transform.position) >= m_max_projectile_distance)
        {
            m_lollipop = true;
            m_coll.radius = m_coll_rad * lollipop_multiplier;
        }
        
    }

    private void FixedUpdate()
    {
        if(!m_rb.isKinematic && !is_retracting)
            m_rb.velocity = m_dir * m_speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
       if (other.tag == "Wall") 
       {
            m_rb.isKinematic = true;
            hook_plant_pos = transform.position;
            on_hook_planted?.Invoke();
       }
    }


    public bool hook_planted { get => hook_plant_pos != null; }
    public bool hook_launched { get => gameObject.activeSelf; }
}
