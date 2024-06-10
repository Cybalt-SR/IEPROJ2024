using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Data;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;

namespace Assets.Scripts.Manager
{
    public class EquipmentManager : Manager_Base<EquipmentManager>, IConsistentDataHolder<EquipmentDictionary>
    {
        protected override void Awake()
        {
            base.Awake();

            IConsistentDataHolder<EquipmentDictionary>.Data = new();
        }

        public bool AddAttachment(string playerId, Attachment attachment)
        {
            return false;
        }
    }
}