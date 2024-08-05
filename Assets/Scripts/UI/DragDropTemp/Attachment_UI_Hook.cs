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
public class Attachment_UI_Hook : MonoBehaviour, IPlayerSpecificUi
{
    public string PlayerAssigned { get; set; }

    public int getIndex(Attachment a)
    {
        var attachmentstorage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);
        return Array.FindIndex<Attachment>(attachmentstorage.ToArray(), attachment => attachment == a); 
    }

    private void OnEnable()
    {
        var attachmentstorage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);

        DragDropController.instance.ReleaseAll();

        int i = 0;
        foreach (var stored_attachment in attachmentstorage)
        {

            DragDropController d = (DragDropController)DragDropController.instance;
            d.InsertAttachmentToUI(stored_attachment);
            i++;
            
        }

        var attachmentequipped = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_equipped_dictionary.GetOrCreate(PlayerAssigned);

        foreach (var equipped_attachment in attachmentequipped)
        {
            //load equipped attachments here

            var which = equipped_attachment.Key;
            var attachment = equipped_attachment.Value;

            SlotManager.instance.slots[which].holdAttachment(attachment);
        }
    }

    //Hook the below functions up

    public void Equip(int index)
    {
        IConsistentDataHolder<PlayerEquipmentData>.Data.Equip(PlayerAssigned, index);
    }

    public void UnEquip(Attachment.Part part)
    {
        IConsistentDataHolder<PlayerEquipmentData>.Data.UnEquip(PlayerAssigned, part);
    }


    #region tempSingleton

    public static Attachment_UI_Hook instance { get; private set; } = null;


    private void Awake()
    {

        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }


    #endregion
}
