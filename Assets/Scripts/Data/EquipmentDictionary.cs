using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Library;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class EquipmentDictionary
    {
        public int storage_limit = 3;
        public SerializableDictionary<string, List<Attachment>> owner_attachments_equipped_dictionary = new("");
        public SerializableDictionary<string, List<Attachment>> owner_attachments_storage_dictionary = new("");
        public SerializableDictionary<string, string> owner_secondary_dictionary = new("");
    }
}