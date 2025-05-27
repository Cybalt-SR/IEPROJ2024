using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AbilityOP
{
    public class SecondaryManager : Manager_Base<SecondaryManager>
    {
        [SerializeField] private Secondary m_equipped;
        [SerializeField] private UnityEvent OnSecondaryEquip;

        public void EquipSecondary(Secondary secondary)
        {
            m_equipped = secondary;
        }

        public void UnequipSecondary()
        {
            m_equipped = null;
        }


    }
}
