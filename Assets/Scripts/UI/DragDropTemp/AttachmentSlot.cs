using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets.Scripts.Manager;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Library;
using UnityEngine.Events;

public class AttachmentSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{

    [SerializeField] private Image i;

    private Attachment.Part part;
    public Attachment.Part Part { get { return part; } set { part = value; } }

    private Attachment held = null;
    private Sprite empty_sprite = null;

    public readonly UnityEvent<Attachment> OnUnequip = new();
    public readonly UnityEvent<GameObject> OnValidDrop = new();
    public readonly UnityEvent<Attachment> OnEquip = new();

    private void Awake()
    {
        empty_sprite = i.sprite;
    }
    public void holdAttachment(Attachment attachment)
    {
        held = attachment;
        i.sprite = held.attachment_icon;
    }

    private void UnEquip()
    {
        OnUnequip.Invoke(held);

        i.sprite = empty_sprite;
        held = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;

        if (dragged == null || dragged.GetComponent<DragDrop_DataHolder>() == null) 
            return;

        print(part);

        Attachment toHold = ISingleton<Clone_DataHolder>.Instance.Data;


        if (toHold.part != part)
            return;

        //if an attachment is equipped, unequip it first
        if(held != null)
            UnEquip();
       
        holdAttachment(toHold);
        ISingleton<Clone_DataHolder>.Instance.Data = null;

        OnEquip.Invoke(toHold);
        OnValidDrop.Invoke(dragged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UnEquip();
        }

    }
}
