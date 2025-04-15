using Assets.Scripts.Data;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Data.Progression;
using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// interface connecting dragdrop UI to the attachment data
/// </summary>
[RequireComponent(typeof(DragDropController))]
public class Attachment_UI_Hook : MonoBehaviour, IPlayerSpecificUi
{
    [SerializeField] private DragDropController mDragDropController = null;
    public string PlayerAssigned { get; set; }

    private void Awake()
    {
        mDragDropController = GetComponent<DragDropController>();
    }

    private void Start()
    {
        mDragDropController.OnEquip.AddListener(attachment =>
        {
            this.Equip(getIndex(attachment));
        });

        mDragDropController.OnUnequip.AddListener(attachment =>
        {
            if (attachment == null)
                return;

            UnEquip(attachment.part);
        });
    }

    public int getIndex(Attachment a)
    {
        var attachmentstorage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);
        return Array.FindIndex<Attachment>(attachmentstorage.ToArray(), attachment => attachment == a); 
    }

    private void OnEnable()
    {
        var attachmentstorage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);

        mDragDropController.ReleaseAll();

        int i = 0;
        foreach (var stored_attachment in attachmentstorage)
        {
            mDragDropController.ReturnToUI(stored_attachment);
            i++;
            
        }

        var attachmentequipped = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_equipped_dictionary.GetOrCreate(PlayerAssigned);

        foreach (var equipped_attachment in attachmentequipped)
        {
            //load equipped attachments here

            var which = equipped_attachment.Key;
            var attachment = equipped_attachment.Value;

            mDragDropController.slots[which].holdAttachment(attachment);
        }
    }

    //Hook the below functions up

    public void Equip(int index)
    {
        IConsistentDataHolder<PlayerEquipmentData>.Data.Equip(PlayerAssigned, index);

        ActionWaiter.Broadcast("equip_attachment", null, out _);
    }

    public void UnEquip(Attachment.Part part)
    {
        IConsistentDataHolder<PlayerEquipmentData>.Data.UnEquip(PlayerAssigned, part);
    }
}
