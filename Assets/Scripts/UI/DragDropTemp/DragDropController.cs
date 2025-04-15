using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Manager;
using Assets.Scripts.Library;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Data.Pickup;
using UnityEditor;
using UnityEngine.Events;

public class DragDropController : GameObjectPoolManager
{
    [SerializeField] public SerializableDictionary<Attachment.Part, AttachmentSlot> slots;

    public readonly UnityEvent<Attachment> OnUnequip = new();
    public readonly UnityEvent<Attachment> OnEquip = new();

    private void Start()
    {
        foreach (var slot in slots)
        {
            slot.Value.Part = slot.Key;
            slot.Value.OnUnequip.AddListener(attachment =>
            {
                ReturnToUI(attachment);
                OnUnequip.Invoke(attachment);
            });
            slot.Value.OnValidDrop.AddListener(dragged =>
            {
                this.Release(dragged);
            });
            slot.Value.OnEquip.AddListener(OnEquip.Invoke);
        }
    }

    protected override void attachComponents(GameObject go)
    {
        go.AddComponent<DragDrop_DataHolder>();
    }

    public override void ReleaseAll()
    {
        foreach(var i in pool)
        {
            DragDrop_DataHolder d = i.GetComponent<DragDrop_DataHolder>();
            d.Data = null;
            i.SetActive(false);
        }
    }

    public void ReturnToUI(Attachment data)
    {
        if (data == null)
            return;

        GameObject poolable = RequestOrCreate();
        DragDrop_DataHolder d = poolable.GetComponent<DragDrop_DataHolder>();
        d.Data = data;
        d.injectToUI();
    }
}
