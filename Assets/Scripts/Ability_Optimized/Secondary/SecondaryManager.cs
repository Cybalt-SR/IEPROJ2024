using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Controller;

namespace AbilityOP
{
    public class SecondaryManager : MonoSingleton<SecondaryManager>
    {
        [SerializeField] private Secondary m_equipped;
        [SerializeField] private UnityEvent<Secondary> OnSecondaryEquip;
        [SerializeField] private UnityEvent<Secondary> OnSecondaryUnequip;

        private Task m_shot_execution;
        private Task m_ability_execution;

        public bool hasEquipped { get => m_equipped != null;  }
        public Secondary currentlyEquipped { get => m_equipped; }

        [Header("Debug")]
        [SerializeField] private Secondary debugSecondary;

        private void Start()
        {
            EquipSecondary(debugSecondary);
        }

        //modify this to receive a gameobject instead so there's no dependency on the playercontroller class
        public void EquipSecondary(Secondary secondary)
        {

            if (secondary == null)
            {
                Debug.LogWarning("[WARNING] No Secondary to Equip");
                return;
            }

            GameObject player = PlayerController.GetFirst().gameObject;

            bool equipSuccess;

            equipSuccess = AbilityManager.Instance.RequestAbility(player, secondary.shot_effect_type);
            equipSuccess = AbilityManager.Instance.RequestAbility(player, secondary.secondary_ability_type);

            if (!equipSuccess)
            {
                Debug.LogError("[ERROR] Equip Unsuccessful.");
                AbilityManager.Instance.ReleaseAbilities(player);
                return;
            }
 
            m_equipped = secondary;
            OnSecondaryEquip?.Invoke(secondary);
  
        }

        public void UnequipSecondary()
        {
            if(m_equipped == null)
            {
                Debug.LogWarning("[WARNING] No Secondary to Unequip");
                return;
            }

            GameObject player = PlayerController.GetFirst().gameObject;
            OnSecondaryUnequip?.Invoke(m_equipped);
            m_equipped = null;
            AbilityManager.Instance.ReleaseAbilities(player);

        }

        private void RunAbilityParallel(ref Task execution, GameObject caster, string ability_name )
        {
            if (m_equipped && (execution == null || execution.IsCompleted))
                execution = Task.Run(async () =>
                    await AbilityManager.Instance.InvokeAbility(caster, ability_name)
                );
            //else Debug.Log("No");
        }

        public void FireSecondary(GameObject caster)
        {
            RunAbilityParallel(ref m_shot_execution, caster, m_equipped.shot_effect_type);
        }

        public void InvokeSecondaryAbility(GameObject caster)
        {
            RunAbilityParallel(ref m_ability_execution, caster, m_equipped.secondary_ability_type);
        }
       
    }
}
