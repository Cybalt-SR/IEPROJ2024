using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Manager;
using Assets.Scripts.Library;
using Assets.Scripts.Gameplay.Manager;

public class DragDropController : GameObjectPoolManager
{
    [SerializeField] private RectTransform clone;

    public static RectTransform Clone = null;

    public override void ReleaseAll()
    {
        foreach(var i in pool)
        {
            DragDrop d = i.GetComponent<DragDrop>();
            d.AssignedAttachment = null;
            i.SetActive(false);
        }
    }
    //should listen to attachment obtained, should have pool to manage attachment containers

    private void Start()
    {

        if (Clone == null)
            Clone = clone;

        clone.GetComponent<CanvasGroup>().blocksRaycasts = false;

        foreach(Transform t in transform)
        {

            //messy af will fix when other dependencies are done

            DragDrop d = t.gameObject.GetComponent<DragDrop>();

            if(d == null)
                d = t.gameObject.AddComponent<DragDrop>();

            d.Clone = clone;
        }
    }
}
