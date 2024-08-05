using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets.Scripts.Manager;
using Assets.Scripts.Data.Pickup;

public class AttachmentSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{

    [SerializeField] private Image i;

    private Attachment.Part part;
    public Attachment.Part Part { get { return part; } set { part = value; } }

    private Attachment held = null;


    public void holdAttachment(Attachment attachment)
    {
        held = attachment;
        i.sprite = held.attachment_icon;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;

        if (dragged == null || dragged.GetComponent<DragDrop>() == null) 
            return;

        dragged.SetActive(false);


        Attachment toHold = CloneDataHolder.instance.Data;


        if(held.part == part)
        {
            holdAttachment(toHold);
            CloneDataHolder.instance.Data = null;
            DragDropController.instance.Release(dragged);
            //call attachment ui hook here
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
            return;

            //unequip


        i.sprite = null;

        DragDropController d = (DragDropController)DragDropController.instance;
        d.InsertAttachmentToUI(held);
        held = null;

        //Call UnEquip Attachment UI Hook here

    }
}
