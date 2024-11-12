using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Abilities
{
    public class AbilityManager : Manager_Base<AbilityManager>
    {
 
        private Dictionary<string, List<Ability>> abilityHolder = new();

        public Ability RequestAbility(string AbilityID, GameObject requester)
        {

            Ability createNew()
            {
                GameObject holder = Instantiate(Resources.Load("Abilities/" + AbilityID, typeof(GameObject)) as GameObject);
                Ability ability = holder.GetComponent<Ability>();

                holder.name = ability.AbilityData.EffectName;
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

            void ConfigureOwnership(Ability ability)
            {
                ability.SetOwner(requester);
                ability.gameObject.SetActive(true);
                ability.transform.parent = requester.transform;
                ability.transform.localPosition = Vector3.zero;
            }

            if (abilityHolder.ContainsKey(AbilityID) == true)
            {
                List<Ability> poolAtKey = abilityHolder[AbilityID];
                Ability unownedAbility = getUnowned(poolAtKey);
                ConfigureOwnership(unownedAbility);
                return unownedAbility;
            }

            abilityHolder.Add(AbilityID, new List<Ability>());
            Ability newAbility = createNew();
            ConfigureOwnership(newAbility);
            return newAbility;

        }

        public void ReleaseAbility(Ability ability)
        {
            ability.SetOwner(null);
            ability.gameObject.SetActive(false);
            ability.transform.parent = transform;
        }


    }

}

