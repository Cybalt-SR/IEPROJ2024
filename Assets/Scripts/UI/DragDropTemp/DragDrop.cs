using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : AttachmentDataHolder, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public void OnDrag(PointerEventData eventData)
    {
       CloneDataHolder.instance.RectTransform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        CloneDataHolder.instance.gameObject.SetActive(true);
        CloneDataHolder.instance.RectTransform.position = eventData.position;
        GetComponent<CanvasGroup>().alpha = 0.5f;


        CloneDataHolder.instance.Data = Data;
        CloneDataHolder.instance.injectToUI();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CloneDataHolder.instance.RectTransform.position = Vector2.zero;
        CloneDataHolder.instance.gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 1f;
    }

}