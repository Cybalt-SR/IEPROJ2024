using gab_roadcasting;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AbilityOP;
using UnityEngine.UI;

public class SecondaryScreen : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private Image module_icon;

    [Header("Active Screens")]
    [SerializeField] private GameObject secondary_screen;
    [SerializeField] private GameObject nothing_installed;
    private void OnEnable()
    {
        var s = SecondaryManager.Instance.currentlyEquipped;

        secondary_screen.SetActive(s != null);
        nothing_installed.SetActive(s == null);

        if (s)
        {
            gunName.text = s.secondary_name;

            var shot_data = SecondaryManager.Instance.GetData(s.shot_effect_type);
            var ability_data = SecondaryManager.Instance.GetData(s.secondary_ability_type);

            var icon = s.secondary_icon;

            module_icon.sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.one * 0.5f);

        }
       

    }

   
}
 