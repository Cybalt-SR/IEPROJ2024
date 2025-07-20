using AbilityOP;
using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    private void LateUpdate()
    {
      
        handler.tooltip_text.ForceMeshUpdate(true, true);
        handler.tooltip_text.ForceMeshUpdate(true, true);

        var layout_group = handler.tooltip.GetComponentInChildren<VerticalLayoutGroup>();

        LayoutRebuilder.ForceRebuildLayoutImmediate(layout_group.GetComponent<RectTransform>());

        float height = layout_group.padding.top + layout_group.padding.bottom + handler.tooltip_text.preferredHeight + handler.tooltip_title.preferredHeight + layout_group.spacing;
        handler.tooltip.sizeDelta = new Vector2(handler.tooltip.sizeDelta.x, height);
  
    }

    public static void DisableTooltip()
    {
        handler.tooltip.gameObject.SetActive(false);
    }

}
