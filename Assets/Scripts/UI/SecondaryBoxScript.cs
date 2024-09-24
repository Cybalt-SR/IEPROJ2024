using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryBoxScript : MonoBehaviour
{

    private Outline outline;


    [Header("Outline Config")]
    [SerializeField] private float outline_thickness = 5f;

    private void Awake()
    {
        outline= GetComponent<Outline>();
        outline.effectDistance = Vector2.one * outline_thickness;
    }

    public void setSelected(bool value)
    {
       outline.enabled = value;
    }

}
