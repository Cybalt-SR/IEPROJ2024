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

    [Range(1f, 20f)]
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float maxScale;

    [SerializeField] private float expansionOffset = 0.2f;
    [SerializeField] private float disableOffset = 0.5f;

    private MeshRenderer mesh;
    private float growthSpeed;

    private float fadeTimer;

    public Action OnFirstContact;
    public Action OnEnd;

    private bool hasEncounteredEnemy = false;

    private void Awake()
    {
        OnProximityEntered += DoOnFirstContact;
    }

    private void OnEnable()
    {

        IEnumerator DelayedAcceleration()
        {
            yield return new WaitForSeconds(expansionOffset);
            growthSpeed = scaleSpeed;
        }


        growthSpeed = 1.7f;
        fadeTimer = disableOffset;

        hasEncounteredEnemy = false;

        transform.localScale = Vector3.one * initialMultiplier;
        mesh = GetComponent<MeshRenderer>();
 
        Color c = mesh.material.color;
        c.a = 1f;
        mesh.material.color = c;

        StartCoroutine(DelayedAcceleration());

    }

    private void DoOnFirstContact(GameObject contact)
    {
        if (!hasEncounteredEnemy)
        {
            OnFirstContact?.Invoke();
            hasEncounteredEnemy = true;
        }  
    }

    private void Update()
    {

        var scale = transform.localScale;

        if (scale.x < maxScale)
        {
            scale *= 1 + Time.deltaTime * growthSpeed;
            scale.x = Mathf.Min(scale.x, maxScale);
            scale.y = Mathf.Min(scale.y, maxScale);
            scale.z = Mathf.Min(scale.z, maxScale);
            transform.localScale = scale;
        }
        else
        {
            mesh.material.DisableKeyword("_EMISSION");
            fadeTimer -= Time.deltaTime;

            Color c = mesh.material.color;
            c.a = Mathf.Lerp(0, 1, fadeTimer / disableOffset);
            mesh.material.color = c;
            if (mesh.material.color.a == 0f)
            {
                OnEnd?.Invoke();
                collisionList.Clear();
                gameObject.SetActive(false);

            }
               
        }


    }
}
