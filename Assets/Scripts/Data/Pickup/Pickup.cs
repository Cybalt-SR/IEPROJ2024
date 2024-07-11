using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Pickup
{
    public class Pickup : ScriptableObject
    {
        public enum PickupType { attachment, secondary }
        public PickupType pickup_type;
        public Attachment data_attachment;
        //public something secondary;
    }
}