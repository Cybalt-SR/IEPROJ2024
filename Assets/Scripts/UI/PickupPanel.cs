using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PickupPanel : MonoBehaviour, IPlayerSpecificUi
    {
        [SerializeField] private GameObject Panel;
        [SerializeField] private TextMeshProUGUI Panel_text;

        string IPlayerSpecificUi.PlayerAssigned { get; set; }

        private void Update()
        {
            var nearby = PickupManager.Instance.Nearest((this as IPlayerSpecificUi).PlayerAssigned);

            if (nearby == null)
            {
                Panel.SetActive(false);
                return;
            }

            Panel.SetActive(true);
            Panel_text.text = nearby.name;
        }
    }
}