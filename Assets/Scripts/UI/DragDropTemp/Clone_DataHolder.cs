using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clone_DataHolder : AttachmentDataHolder, ISingleton<Clone_DataHolder> { 

    private RectTransform _rectTransform;
    public RectTransform RectTransform { get { return _rectTransform; } }

    private void Awake()
    {
        ISingleton<Clone_DataHolder>.Instance = this;
        _rectTransform = GetComponent<RectTransform>();
    }
}
