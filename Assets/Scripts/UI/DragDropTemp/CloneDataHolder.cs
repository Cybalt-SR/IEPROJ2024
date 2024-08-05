using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneDataHolder : AttachmentDataHolder { 

    private RectTransform _rectTransform;
    public RectTransform RectTransform { get { return _rectTransform; } }

    #region tempSingleton

    public static CloneDataHolder instance { get; private set; } = null;
    private void Awake()
    {
        gameObject.SetActive(true);
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        _rectTransform = GetComponent<RectTransform>();

    }

    private void Start()
    {
        gameObject.SetActive(false);
    }


    #endregion
}
