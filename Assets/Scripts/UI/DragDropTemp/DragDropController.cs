using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Manager;
using Assets.Scripts.Library;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Data.Pickup;

public class DragDropController : GameObjectPoolManager
{
 
    protected override void attachComponents(GameObject go)
    {
        go.AddComponent<DragDrop>();
    }

    public override void ReleaseAll()
    {
        foreach(var i in pool)
        {
            DragDrop d = i.GetComponent<DragDrop>();
            d.Data = null;
            i.SetActive(false);
        }
    }

    public void InsertAttachmentToUI(Attachment data)
    {
        GameObject poolable = RequestOrCreate();
        DragDrop d = poolable.GetComponent<DragDrop>();
        d.Data = data;
        d.injectToUI();

    }
}
