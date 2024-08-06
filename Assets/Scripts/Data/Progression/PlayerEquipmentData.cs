using Assets.Scripts.Controller;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Library;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data.Progression
{
    [Serializable]
    public class PlayerEquipmentData
    {
        public int storage_limit = 10;
        public SerializableDictionary<string, SerializableDictionary<Attachment.Part, Attachment>> owner_attachments_equipped_dictionary = new("", () => new(Attachment.Part.Core1));
        public SerializableDictionary<string, List<Attachment>> owner_attachments_storage_dictionary = new("", () => new());
        public SerializableDictionary<string, string> owner_secondary_dictionary = new("", () => "");

        public void Destroy(string playerId, int storageindex)
        {
            var attachmentstorage = owner_attachments_storage_dictionary.GetOrCreate(playerId);

            attachmentstorage.RemoveAt(storageindex);

            
        }
        public void Equip(string playerId, int storageindex)
        {
            var attachmentstorage = owner_attachments_storage_dictionary.GetOrCreate(playerId);
            var attachmentequipped = owner_attachments_equipped_dictionary.GetOrCreate(playerId);

            if (storageindex >= attachmentstorage.Count)
                return;

            var to_equip = attachmentstorage[storageindex];

            if (attachmentequipped.ContainsKey(to_equip.part) == false)
                attachmentequipped.Add(to_equip.part, to_equip);
            else
            {
                var alr_equipped = attachmentequipped[to_equip.part];

                if (alr_equipped != null)
                    attachmentstorage.Add(alr_equipped);

                attachmentequipped[to_equip.part] = to_equip;
            }

            attachmentstorage.RemoveAt(storageindex);

            PlayerController.SetGunChanged(playerId);
        }

        public void UnEquip(string playerId, Attachment.Part part)
        {
            var attachmentstorage = owner_attachments_storage_dictionary.GetOrCreate(playerId);
            var attachmentequipped = owner_attachments_equipped_dictionary.GetOrCreate(playerId);

            if (attachmentstorage.Count >= 3)
                return;

            if(attachmentequipped.Remove(part, out var unequipped))
            {
                attachmentstorage.Add(unequipped);
            }

            PlayerController.SetGunChanged(playerId);
        }
    }
}