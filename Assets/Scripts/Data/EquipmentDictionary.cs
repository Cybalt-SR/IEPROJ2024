using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public class EquipmentDictionary
    {
        private Dictionary<string, List<Attachment>> owner_attachments_dictionary = new();
        private Dictionary<string, string> owner_secondary_dictionary = new();
    }
}