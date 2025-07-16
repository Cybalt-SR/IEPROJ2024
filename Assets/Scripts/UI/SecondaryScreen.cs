using gab_roadcasting;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AbilityOP;

public class SecondaryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI tooltip;
    [SerializeField] private TextMeshProUGUI shotDescription;
    [SerializeField] private TextMeshProUGUI activeDescription;


    private void OnEnable()
    {
        Debug.Log("Updating Text");
        var s = SecondaryManager.Instance.currentlyEquipped;

        if (s)
        {
            gunName.text = s.secondary_name;
            tooltip.text = s.tooltip;

            var shot_data = SecondaryManager.Instance.GetData(s.shot_effect_type);
            var ability_data = SecondaryManager.Instance.GetData(s.secondary_ability_type);

            shotDescription.text = "Shot:\n" + shot_data.AbilityDescription;
            activeDescription.text = "Ability:\n" + ability_data.AbilityDescription;
        }
        else
        {
            gunName.text =  "[No Equipped Secondaries]";
            tooltip.text = "";
            shotDescription.text = "";
            activeDescription.text = "";
        }


    }

   
}
 