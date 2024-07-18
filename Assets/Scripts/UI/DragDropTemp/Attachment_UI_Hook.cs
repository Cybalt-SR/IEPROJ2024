using Assets.Scripts.Data;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Data.Progression;
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
        var attachmentstorage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);

        //DragDropController.Instance.ReleaseAll();
        ISingleton<DragDropController>.Instance.ReleaseAll();
        foreach (var stored_attachment in attachmentstorage)
        {
            GameObject container = ISingleton<DragDropController>.Instance.RequestOrCreate();
            DragDrop draggable = container.GetComponent<DragDrop>();

            if (draggable == null)
                continue;

            draggable.AssignedAttachment = stored_attachment;

        }

        var attachmentequipped = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_equipped_dictionary.GetOrCreate(PlayerAssigned);

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
        IConsistentDataHolder<PlayerEquipmentData>.Data.Equip(PlayerAssigned, index);
    }

    public void UnEquip(Attachment.Part part)
    {
        IConsistentDataHolder<PlayerEquipmentData>.Data.UnEquip(PlayerAssigned, part);
    }
}
