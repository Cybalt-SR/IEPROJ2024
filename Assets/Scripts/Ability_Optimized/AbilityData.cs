using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilityOP
{
    public abstract class AbilityData : ScriptableObject
    {
        [Header("Details")]
        public string AbilityName;
        public string AbilityDescription;
        public Texture2D AbilityIcon;

        [Header("Stats")]
        public float Cooldown;

        [Header("Configurations")]
        public bool StartCooldownOnCast;
    }

}
