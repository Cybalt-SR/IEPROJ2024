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

    public Attachment[] GetAttachments()
	{
		var attachmentstorage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);
		return attachmentstorage.ToArray();
	}

	public int getIndex(Attachment a)
    {
        var attachmentstorage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);
        return Array.FindIndex<Attachment>(attachmentstorage.ToArray(), attachment => attachment == a); 
    }

	public Attachment GetEquipped(Attachment.Part part)
	{
		return IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_equipped_dictionary.GetOrCreate(PlayerAssigned).GetOrCreate(part);
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
