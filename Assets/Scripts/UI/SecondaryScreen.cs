using gab_roadcasting;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondaryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI tooltip;
    [SerializeField] private TextMeshProUGUI shotDescription;
    [SerializeField] private TextMeshProUGUI passiveDescription;
    [SerializeField] private TextMeshProUGUI activeDescription;


    private void OnEnable()
    {
        Debug.Log("Updating Text");
        var s = SecondaryManager.Instance.CurrentlyEquipped;

        gunName.text = s.SecondaryName;
        tooltip.text = s.ToolTip;
        shotDescription.text = "[Shot]: " + s.ShotEffect.AbilityData.ActiveDescription;
        passiveDescription.text = "[Passive]: " + s.SecondaryAbility.AbilityData.PassiveDescription;
        activeDescription.text = "[Active]: " + s.SecondaryAbility.AbilityData.ActiveDescription;
    }

   
}
