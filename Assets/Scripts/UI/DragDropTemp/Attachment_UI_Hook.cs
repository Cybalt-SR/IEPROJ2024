using Assets.Scripts.Data;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// interface connecting dragdrop UI to the attachment data
/// </summary>
public class Attachment_UI_Hook : MonoBehaviour, IPlayerSpecificUi
{
    public string PlayerAssigned { get; set; }

    private void OnEnable()
    {
        var attachmentstorage = IConsistentDataHolder<EquipmentDictionary>.Data.owner_attachments_storage_dictionary[PlayerAssigned];

        foreach (var stored_attachment in attachmentstorage)
        {
            //load attachment storage here
        }

        var attachmentequipped = IConsistentDataHolder<EquipmentDictionary>.Data.owner_attachments_equipped_dictionary[PlayerAssigned];

        foreach (var equipped_attachment in attachmentequipped)
        {
            //load equipped attachments here

            var which = equipped_attachment.Key;
            var attachment = equipped_attachment.Value;
        }
    }

    //Hook the below functions up

    public void Equip(int index)
    {
        IConsistentDataHolder<EquipmentDictionary>.Data.Equip(PlayerAssigned, index);
    }

    public void UnEquip(Attachment.Part part)
    {
        IConsistentDataHolder<EquipmentDictionary>.Data.UnEquip(PlayerAssigned, part);
    }
}
