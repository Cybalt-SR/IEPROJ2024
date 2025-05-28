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

        public void EquipSecondary(Secondary secondary)
        {

            GameObject player = PlayerController.GetFirst().gameObject;

            bool equipSuccess = false;

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
            GameObject player = PlayerController.GetFirst().gameObject;
            OnSecondaryUnequip?.Invoke(m_equipped);
            m_equipped = null;
            AbilityManager.Instance.ReleaseAbilities(player);
        }
       
        //Temp - Hook this up with the project's input system
        private void Update()
        {
            if (!hasEquipped) return;

            if (Input.GetMouseButtonDown(1))
            {
                GameObject player = PlayerController.GetFirst().gameObject;
                if (m_shot_execution == null || m_shot_execution.IsCompleted)
                    m_shot_execution = AbilityManager.Instance.InvokeAbility(player, m_equipped.shot_effect_type);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject player = PlayerController.GetFirst().gameObject;
                if (m_ability_execution == null || m_ability_execution.IsCompleted)
                    m_ability_execution = AbilityManager.Instance.InvokeAbility(player, m_equipped.secondary_ability_type);
            }
        }

    }
}
