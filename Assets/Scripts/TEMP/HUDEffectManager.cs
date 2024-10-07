using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject InvisibilityOverlay;

    private void Start()
    {
        InvisibilityOverlay.SetActive(false);
    }


    public void SetInvisibilityOverlay(bool value) => InvisibilityOverlay.SetActive(value);


    #region singleton

    public static HUDEffectManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    #endregion
}
