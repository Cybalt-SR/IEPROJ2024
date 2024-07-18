using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets.Scripts.Manager;
public class AttachmentSlot : MonoBehaviour, IDropHandler
{
    //temp
    [SerializeField] private Image i;
    [SerializeField] private string which;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;

        if (dragged == null || dragged.GetComponent<DragDrop>() == null) 
            return;

        dragged.SetActive(false);
        i.sprite = DragDropController.Clone.GetComponent<CloneDataHolder>().Data.attachment_icon;

        

        //remove attachment reference and return it to pool
    }
}
