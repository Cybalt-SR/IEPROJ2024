using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityOP
{
    public class AbilityData : ScriptableObject
    {
        [Header("Details")]
        public string AbilityName;
        [TextArea(2,10)] public string AbilityDescription;
        public Texture2D AbilityIcon;

        [Header("Stats")]
        public float Cooldown;

        [Header("Configurations")]
        public bool StartCooldownOnCast;
        public bool UpdateTiedToState;
    }

}
