using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using gab_roadcasting;

namespace Abilities
{
    public class AbilityManager : Manager_Base<AbilityManager>
    {

        private Dictionary<string, Ability> abilityHolder = new();
        [SerializeField]private Ability secondaryActive;


        private void LoadSecondary()
        {

        }

        protected override void Awake()
        {
            base.Awake();
            EventBroadcasting.AddListener(EventNames.ABILITY_EVENTS.ON_ABILITY_ACTIVATION, Activate);
           
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                Activate(null);
        }

        private void Activate(Dictionary <string, object> parameters)
        {
            if (secondaryActive == null)
                return;

            if (secondaryActive.Cooldown.isCooldownFinished())
                secondaryActive.Activate();
                         
        }


        //will convert this into an OnSecondaryChanged event
        private void LoadAbility(Ability prefab)
        {

            if (abilityHolder.ContainsKey(prefab.AbilityData.AbilityID) == false)
            {

                GameObject holder = Instantiate(prefab.gameObject);
                Ability ability = holder.GetComponent<Ability>();

                holder.name = ability.AbilityData.EffectName;
                holder.transform.parent = transform;

                abilityHolder.Add(ability.AbilityData.AbilityID, ability);
             
            }

            secondaryActive = abilityHolder[prefab.AbilityData.AbilityID];

        }

    }

}
