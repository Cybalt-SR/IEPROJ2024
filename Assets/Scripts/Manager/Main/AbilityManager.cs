using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using gab_roadcasting;
using UnityEngine.WSA;
using UnityEditor.Playables;

namespace Abilities
{
    public class AbilityManager : Manager_Base<AbilityManager>
    {
 
        private Dictionary<string, List<Ability>> abilityHolder = new();

        public Ability RequestAbility(string AbilityID, GameObject requester)
        {

            Ability createNew()
            {
                GameObject holder = Resources.Load("Abilities/" + AbilityID, typeof(GameObject)) as GameObject;
                Ability ability = holder.GetComponent<Ability>();

                holder.name = ability.AbilityData.EffectName;
                holder.transform.parent = transform;

                abilityHolder[AbilityID].Add(ability);

                return ability;
            }

            Ability getUnowned(List<Ability> pool)
            {
                foreach (Ability ability in pool)
                    if (ability.IsOwned == false)
                        return ability;

                return createNew();
            }

            if (abilityHolder.ContainsKey(AbilityID) == true)
            {
                List<Ability> poolAtKey = abilityHolder[AbilityID];
                Ability unownedAbility = getUnowned(poolAtKey);
                unownedAbility.SetOwner(requester);
                unownedAbility.gameObject.SetActive(true);
                return unownedAbility;
            }

            abilityHolder.Add(AbilityID, new List<Ability>());
            Ability newAbility = createNew();     
            newAbility.SetOwner(requester);
            newAbility.gameObject.SetActive(true);
            return newAbility;

        }

        public void ReleaseAbility(Ability ability)
        {
            ability.SetOwner(null);
            ability.gameObject.SetActive(false);
        }


    }

}

