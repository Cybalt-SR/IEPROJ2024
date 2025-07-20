using AbilityOP;
using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecondaryTooltipHandler : MonoBehaviour
{

    private static SecondaryTooltipHandler handler;
 
    [SerializeField] private TextMeshProUGUI tooltip_title;
    [SerializeField] private TextMeshProUGUI tooltip_text;
    [SerializeField] private RectTransform tooltip;
    private void Awake()
    {
        if (handler == null)
            handler = this;

        DisableTooltip();
    }

    public static void RequestTooltip(RectTransform requester, string title)
    {
        var pos = handler.tooltip.anchoredPosition;
        pos.y = requester.anchoredPosition.y;
        handler.tooltip.anchoredPosition = pos;


        handler.tooltip_title.text = title;

        var curr = SecondaryManager.Instance.currentlyEquipped;

        //ermm
        switch (title)
        {
            case "Secondary":
                handler.tooltip_text.text = curr.tooltip;
                break;
            case "Shot":
                var shot_data = AbilityManager.Instance.GetAbilityData(PlayerController.GetFirst().gameObject, curr.shot_effect_type) as AbilityData;
                handler.tooltip_text.text = shot_data.AbilityDescription;
                break;
            case "Ability":
                var ability_data = AbilityManager.Instance.GetAbilityData(PlayerController.GetFirst().gameObject, curr.secondary_ability_type) as AbilityData;
                handler.tooltip_text.text = ability_data.AbilityDescription;
                break;
        }

        handler.tooltip.gameObject.SetActive(true);
    }

    public static void DisableTooltip()
    {
        handler.tooltip.gameObject.SetActive(false);
    }

}
