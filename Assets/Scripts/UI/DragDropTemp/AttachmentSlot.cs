using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttachmentSlot : MonoBehaviour, IDropHandler
{
    //temp
    [SerializeField] private Image i;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;

        if (dragged == null || dragged.GetComponent<DragDrop>() == null) 
            return;

        dragged.SetActive(false);
        i.sprite = DragDropUI.Clone.GetComponent<CloneDataHolder>().Image.sprite;

        

        //remove attachment reference and return it to pool
    }
}
