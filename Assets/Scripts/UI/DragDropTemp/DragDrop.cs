using Assets.Scripts.Data.Pickup;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private RectTransform _clone;
    public RectTransform Clone { get { return _clone; } set { _clone = value; } }

    private Attachment assignedAttachment = null;
    public Attachment AssignedAttachment { get { return assignedAttachment;} set { assignedAttachment = value; } }



    public void applyAttachementData(GameObject toApply)
    {

    }










    public void OnDrag(PointerEventData eventData)
    {
        _clone.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _clone.gameObject.SetActive(true);
        _clone.position = GetComponent<RectTransform>().position;
        GetComponent<CanvasGroup>().alpha = 0.5f;


        CloneDataHolder d = _clone.GetComponent<CloneDataHolder>();
        d.Data = assignedAttachment;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _clone.position = Vector2.zero;
        _clone.gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 1f;
    }

}