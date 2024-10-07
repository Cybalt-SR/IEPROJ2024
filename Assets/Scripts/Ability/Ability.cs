using System;
using System.Collections.Generic;
using UnityEngine;

using gab_roadcasting;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {

        [SerializeField] protected GameObject owner;

        public bool IsOwned { get { return owner != null; } }


        [SerializeField] protected AbilityData abilityData;
        public AbilityData AbilityData { get { return abilityData; } }

        [Header("Cooldown Debugging")]
        [SerializeField] protected CooldownHandler cooldown;
        public CooldownHandler Cooldown { get { return cooldown; } }

        protected Dictionary<string, Action<Dictionary<string, object>>> passiveHandler = new();


        public void SetOwner(GameObject unit)
        {
            owner = unit;
        }

        protected virtual void Update()
        {
            cooldown.Update();
        }

        public void Activate()
        {
            if(IsOwned == false)
            {
                Debug.LogError(new Exception($"{AbilityData.EffectName} has no Owner!"));
                return;
            }

            if (cooldown.isCooldownFinished())
            {
                Cast();
                cooldown.Start();
            }
        }

        protected virtual void Awake()
        {
            Initialize();

            foreach (var passive in passiveHandler)
                EventBroadcasting.AddListener(passive.Key, passive.Value);

            cooldown = new CooldownHandler(abilityData.Cooldown, abilityData.AbilityID);
        }

        protected abstract void Initialize();

        protected abstract void Cast();
    }


}
