using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Slime_Gun_Adhesive : MonoBehaviour
{
    public float sliming_distance = 1f;
    public int max_carry_count;

    private List<UnitController> enemies_trapped = new();
    private int carry_count = 0;

    public Action<List<UnitController>> on_detonation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && other.GetComponent<BossController>() == null)
        {
            enemies_trapped.Add(other.GetComponent<UnitController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var controller = other.GetComponent<UnitController>();
        if (enemies_trapped.Contains(controller))
        {
            enemies_trapped.Remove(controller);
        }
    }

    private void Update()
    {
        foreach(var e in enemies_trapped)
        {
            if (!e.gameObject.activeSelf)
                continue;

            if (Vector3.Distance(e.transform.position, transform.position) < sliming_distance)
                SetUnitEnabled(e.gameObject, false);
        }
    }

    private void SetUnitEnabled(GameObject go, bool value)
    {
        var controller = go.GetComponent<UnitController>();
        var coll = go.GetComponent<Collider>();
        var rb = go.GetComponent<Rigidbody>();

        go.transform.parent = value ? null : transform;
        controller.enabled = value;
        coll.enabled = value;
        rb.isKinematic = !value;

        carry_count += Mathf.Max(value ? -1 : 1, 0);
    }

    public void Unload(bool will_detonate)
    {
        foreach (var e in enemies_trapped)
        {
            SetUnitEnabled(e.gameObject, true);
        }
        if (will_detonate)
            on_detonation?.Invoke(enemies_trapped);
      
        enemies_trapped.Clear();
        gameObject.SetActive(false);
    }

}
