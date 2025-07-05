using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{


    private void Update()
    {
        var rb = PlayerController.GetFirst().GetComponent<Rigidbody>();

        //rb.AddForce(Vector3.up * 3);
        rb.velocity = Vector3.up * 3;
        Debug.Log(rb.velocity);
        PlayerController.GetFirst().enabled = false;
    }
}
