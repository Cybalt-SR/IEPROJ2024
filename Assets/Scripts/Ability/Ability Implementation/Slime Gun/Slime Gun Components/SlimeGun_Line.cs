using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGun_Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameObject playerObject;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.renderQueue = 3100;
        playerObject = PlayerController.GetFirst().gameObject;

        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, playerObject.transform.position);
        lineRenderer.SetPosition(1, transform.position);  
    }



}
