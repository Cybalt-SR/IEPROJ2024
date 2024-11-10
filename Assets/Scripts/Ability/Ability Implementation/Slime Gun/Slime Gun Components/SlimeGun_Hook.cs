using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGun_Hook : MonoBehaviour
{

    private Vector3? vectorLock = null;
    private Rigidbody rb;


    public bool isLocked { get => vectorLock.HasValue; }
    public Action onGrapplerHook;
    public Action onGrapplerUnhooked;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
      
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        vectorLock = collision.transform.position;
        onGrapplerHook?.Invoke();

    }

    private void Update()
    {
        if(vectorLock != null)
            transform.position = vectorLock.Value;     
    }

    public void ClearLock()
    {
        vectorLock = null;
        rb.isKinematic=false;
        onGrapplerUnhooked?.Invoke();
    }

}
