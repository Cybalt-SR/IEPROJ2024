using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop_DataHolder : AttachmentDataHolder, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public void OnDrag(PointerEventData eventData)
    {
       ISingleton<Clone_DataHolder>.Instance.RectTransform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        ISingleton<Clone_DataHolder>.Instance.gameObject.SetActive(true);
        ISingleton<Clone_DataHolder>.Instance.RectTransform.position = eventData.position;
        GetComponent<CanvasGroup>().alpha = 0.5f;


        ISingleton<Clone_DataHolder>.Instance.Data = Data;
        ISingleton<Clone_DataHolder>.Instance.injectToUI();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ISingleton<Clone_DataHolder>.Instance.RectTransform.position = Vector2.zero;
        ISingleton<Clone_DataHolder>.Instance.gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 1f;
    }

}