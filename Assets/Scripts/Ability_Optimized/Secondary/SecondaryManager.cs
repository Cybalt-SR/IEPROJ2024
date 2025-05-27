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
        [SerializeField] private UnityEvent OnSecondaryEquip;

        private Task m_shot_execution;
        private Task m_ability_execution;

        public void EquipSecondary(Secondary secondary) => m_equipped = secondary;
        public void UnequipSecondary() => m_equipped = null;
       

        //Temp - Hook this up with the project's input system
        private void Update()
        {
            if (m_equipped == null) return;

            if (Input.GetMouseButtonDown(1))
            {
                if(m_shot_execution == null || m_shot_execution.IsCompleted)
                    m_shot_execution = AbilityManager.Instance.InvokeAbility(PlayerController.GetFirst().gameObject, m_equipped.shot_effect_type);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (m_ability_execution == null || m_ability_execution.IsCompleted)
                    m_ability_execution = AbilityManager.Instance.InvokeAbility(PlayerController.GetFirst().gameObject, m_equipped.secondary_ability_type);
            }
        }

    }
}
