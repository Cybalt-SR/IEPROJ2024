using Assets.Scripts.Controller;
using gab_roadcasting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.GridLayoutGroup;

public class TEMP_Attack_Anim : ProximityChecker
{
    [SerializeField] private float initialMultiplier = 2f;

    [Range(10f, 100f)]
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float maxScale;

    [SerializeField] private float expansionOffset = 0.2f;
    [SerializeField] private float disableOffset = 0.5f;

    private MeshRenderer mesh;
    private float growthSpeed;

    private float fadeTimer;


    //temp
    public Action doOnEnd;


    private void OnEnable()
    {

        IEnumerator DelayedAcceleration()
        {
            yield return new WaitForSeconds(expansionOffset);
            growthSpeed = scaleSpeed;
        }


        growthSpeed = 1.7f;
        fadeTimer = disableOffset;


        transform.localScale = Vector3.one * initialMultiplier;
        mesh = GetComponent<MeshRenderer>();
 
        Color c = mesh.material.color;
        c.a = 1f;
        mesh.material.color = c;

        StartCoroutine(DelayedAcceleration());

    }


    private void Update()
    {

        var scale = transform.localScale;

        if (scale.x < maxScale)
        {
            scale *= 1 + Time.deltaTime * scaleSpeed;
            scale.y = initialMultiplier;
            transform.localScale = scale;

        }
        else
        {
            fadeTimer -= Time.deltaTime;

            Color c = mesh.material.color;
            c.a = Mathf.Lerp(0, 1, fadeTimer / disableOffset);
            mesh.material.color = c;
            if (mesh.material.color.a == 0f)
            {
                doOnEnd?.Invoke();
                collisionList.Clear();
                gameObject.SetActive(false);

            }
               
        }


    }
}
