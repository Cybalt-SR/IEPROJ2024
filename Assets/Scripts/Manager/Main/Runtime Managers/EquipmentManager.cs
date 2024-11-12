using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Data;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using UnityEngine;
using Assets.Scripts.Data.Progression;

namespace Assets.Scripts.Manager
{
    public class EquipmentManager : Manager_Base<EquipmentManager>, IConsistentDataHolder<PlayerEquipmentData>
    {
        [SerializeField] private PlayerEquipmentData m_data = new();

        protected override void Awake()
        {
            base.Awake();

            IConsistentDataHolder<PlayerEquipmentData>.Data = m_data;
        }

        public static bool PickupAttachment(string playerId, Attachment attachment)
        {
            var data = IConsistentDataHolder<PlayerEquipmentData>.Data;
            var dict = data.owner_attachments_storage_dictionary;

            if (dict.ContainsKey(playerId) == false)
                dict.Add(playerId, new());

            if (dict[playerId].Count >= data.storage_limit)
                return false;

            dict[playerId].Add(attachment);

            return true;
        }
    }
}