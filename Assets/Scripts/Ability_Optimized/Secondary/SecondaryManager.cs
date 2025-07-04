using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Controller;
using System;

namespace AbilityOP
{
    public class SecondaryManager : MonoSingleton<SecondaryManager>
    {
        [SerializeField] private Secondary m_equipped;
        [SerializeField] private UnityEvent<Secondary> OnSecondaryEquip;
        [SerializeField] private UnityEvent<Secondary> OnSecondaryUnequip;

        [Header("Debug")]
        [SerializeField] private Secondary debugSecondary;

        private void Start()
        {
            Debug.developerConsoleVisible = true;
            EquipSecondary(debugSecondary);
        }

        public void EquipSecondary(Secondary secondary)
        {

            if (secondary == null)
            {
                Debug.LogWarning("[WARNING] No Secondary to Equip");
                return;
            }

            if (m_equipped != null)
                UnequipSecondary();
            
            GameObject player = PlayerController.GetFirst().gameObject;

            bool equipSuccess = AbilityManager.Instance.RequestAbility(player, secondary.shot_effect_type);
            equipSuccess = AbilityManager.Instance.RequestAbility(player, secondary.secondary_ability_type);

            if (!equipSuccess)
            {
                Debug.LogError("[ERROR] Equip Unsuccessful.");
                AbilityManager.Instance.ReleaseAbilities(player);
                GameObject.Instantiate(Resources.Load("The Debug Cube") as GameObject).transform.position = Vector3.zero;
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

        public void FireSecondary(GameObject caster)
        {
            if (m_equipped)
                AbilityManager.Instance.InvokeAbility(caster, m_equipped.shot_effect_type);
        }

        public void InvokeSecondaryAbility(GameObject caster)
        {
           if (m_equipped)
                AbilityManager.Instance.InvokeAbility(caster, m_equipped.secondary_ability_type);
        }

        public bool hasEquipped { get => m_equipped != null; }
        public Secondary currentlyEquipped { get => m_equipped; }



        //debug

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                UnequipSecondary();
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                EquipSecondary(debugSecondary);
            }
        }

    }
}
