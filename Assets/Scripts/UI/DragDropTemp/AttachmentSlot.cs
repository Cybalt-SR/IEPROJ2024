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

    private void UnEquip()
    {
        //Call UnEquip Attachment UI Hook here
        Attachment_UI_Hook.instance.UnEquip(part);

        DragDropController d = (DragDropController)DragDropController.instance;
        d.InsertAttachmentToUI(held);
  
        i.sprite = null;
        held = null;
    }
    public void OnDrop(PointerEventData eventData)
    {

        GameObject dragged = eventData.pointerDrag;

        if (dragged == null || dragged.GetComponent<DragDrop>() == null) 
            return;

        print(part);

        Attachment toHold = CloneDataHolder.instance.Data;


        if (toHold.part != part)
            return;

        //if an attachment is equipped, unequip it first
        if(held != null)
            UnEquip();
       
        holdAttachment(toHold);
        CloneDataHolder.instance.Data = null;

        int index = Attachment_UI_Hook.instance.getIndex(toHold);
        print(index);
        Attachment_UI_Hook.instance.Equip(index);

        DragDropController.instance.Release(dragged);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UnEquip();
        }

    }
}
